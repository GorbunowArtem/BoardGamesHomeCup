using System;
using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Players;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Presentation.Players.Commands.DeletePlayer;
using HorCup.Presentation.Players.Queries.GetById;
using HorCup.Presentation.Players.Queries.SearchPlayers;
using HorCup.Presentation.Responses;
using HorCup.Presentation.Services.Players;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers
{
	[ApiController]
	[Route("players")]
	public class PlayersController : ControllerBase
	{
		private readonly ISender _sender;
		private readonly IPlayersService _playersService;

		public PlayersController(ISender sender, IPlayersService playersService)
		{
			_sender = sender;
			_playersService = playersService;
		}

		[HttpGet]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		public async Task<ActionResult<PagedSearchResponse<PlayerViewModel>>> Search(
			[FromQuery] SearchPlayersQuery query)
		{
			var (items, total) = await _sender.Send(query);

			return Ok(new PagedSearchResponse<PlayerViewModel>(items, total));
		}

		[HttpPost]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		[ProducesResponseType((int) HttpStatusCode.Created)]
		public async Task<ActionResult<Guid>> Add([FromBody] AddPlayerCommand command)
		{
			var id = await _sender.Send(command);

			return CreatedAtAction(nameof(Add), new {id}, command);
		}

		[HttpHead]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public async Task<IActionResult> IsNicknameUnique(string nickname)
		{
			var isUnique = await _playersService.IsNicknameUniqueAsync(nickname);

			if (isUnique)
			{
				return Ok();
			}

			return Conflict();
		}

		[HttpGet("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<PlayerViewModel>> Get(Guid id)
		{
			return Ok(await _sender.Send(new GetPlayerByIdQuery(id)));
		}

		[HttpDelete("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _sender.Send(new DeletePlayerCommand(id));

			return Ok();
		}
	}
}