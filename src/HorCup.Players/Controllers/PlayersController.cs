using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using HorCup.Infrastructure.Responses;
using HorCup.Players.Commands.AddPlayer;
using HorCup.Players.Commands.DeletePlayer;
using HorCup.Players.Commands.EditPlayer;
using HorCup.Players.Queries.GetById;
using HorCup.Players.Queries.SearchPlayers;
using HorCup.Players.Services.Players;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayerViewModel = HorCup.Players.ViewModels.PlayerViewModel;

namespace HorCup.Players.Controllers
{
	[ExcludeFromCodeCoverage]
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

			return CreatedAtAction(nameof(Add), new {id}, id.ToString());
		}

		[HttpPatch("{id:Guid}")]
		[ProducesResponseType((int) HttpStatusCode.NoContent)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditPlayerCommand command)
		{
			await _sender.Send(command);

			return NoContent();
		}

		[HttpHead]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public async Task<IActionResult> IsNicknameUnique(string nickname, Guid? id)
		{
			var isUnique = await _playersService.IsNicknameUniqueAsync(nickname, id);

			if (isUnique)
			{
				return Ok();
			}

			return Conflict();
		}

		[HttpGet("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<PlayerViewModel>> Get([FromRoute] Guid id)
		{
			return Ok(await _sender.Send(new GetPlayerByIdQuery(id)));
		}

		[HttpDelete("{id:Guid}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			await _sender.Send(new DeletePlayerCommand(id));

			return NoContent();
		}
	}
}