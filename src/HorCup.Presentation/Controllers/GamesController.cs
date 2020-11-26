using System;
using System.Threading.Tasks;
using HorCup.Presentation.Game.Commands.AddGame;
using HorCup.Presentation.Responses;
using HorCup.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers
{
	[ApiController]
	[Route("games")]
	public class GamesController: ControllerBase
	{
		[HttpGet]
		public Task<ActionResult<PagedSearchResponse<GameViewModel>>> SearchGames()
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public Task<GameViewModel> Add(AddGameCommand command)
		{
			throw new NotImplementedException();
		}
	}
}