using System;
using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Games.Commands.AddGame;
using HorCup.Presentation.Responses;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers
{
	[ApiController]
	[Route("games")]
	public class GamesController : ControllerBase
	{
		private readonly ISender _sender;

		public GamesController(ISender sender)
		{
			_sender = sender;
		}

		[HttpGet]
		public Task<ActionResult<PagedSearchResponse<GameViewModel>>> SearchGames()
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		[ProducesResponseType((int) HttpStatusCode.Created)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public async Task<ActionResult<GameViewModel>> Add(AddGameCommand command)
		{
			var game = await _sender.Send(command);

			return CreatedAtAction(nameof(Add), $"/games/{game.Id}", game);
		}
	}
}