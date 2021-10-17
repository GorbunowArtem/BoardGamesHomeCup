using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Akkatecture.Akka;
using HorCup.Games.Commands.AddGame;
using HorCup.Games.Commands.DeleteGame;
using HorCup.Games.Commands.EditGame;
using HorCup.Games.Models;
using HorCup.Games.Queries;
using HorCup.Games.Queries.IsTitleUnique;
using HorCup.Games.Queries.SearchGames;
using HorCup.Games.Requests;
using HorCup.Games.ViewModels;
using HorCup.Infrastructure.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Games.Controllers
{
	[ExcludeFromCodeCoverage]
	[ApiController]
	[Route("games")]
	public class GamesController : ControllerBase
	{
		private readonly ISender _sender;
		private readonly ActorRefProvider<GameManager> _provider;
		private readonly IGamesQueryHandler _queryHandler;
		public GamesController(ISender sender, ActorRefProvider<GameManager> provider,
			IGamesQueryHandler queryHandler)
		{
			_sender = sender;
			_provider = provider;
			_queryHandler = queryHandler;
		}

		[HttpGet]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		public async Task<ActionResult<PagedSearchResponse<GameViewModel>>> SearchGames(
			[FromQuery] SearchGamesQuery query)
		{
			var (items, total) = await _sender.Send(query);

			return Ok(new PagedSearchResponse<GameViewModel>(items, total));
		}

		[HttpGet("{id:Guid}")]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		public async Task<ActionResult<GameDetailsViewModel>> GetById([FromRoute] Guid id)
		{
			var game = await _queryHandler.Get(id);

			return Ok(game);
		}

		[HttpPost]
		[ProducesResponseType((int) HttpStatusCode.Created)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public ActionResult<Guid> Add([FromBody] GameViewModel game)
		{
			var id = GameId.New;
			
			var command = new AddGameCommand(id, game.Title, game.MaxPlayers, game.MinPlayers);
			
			_provider.Tell(command);
			return CreatedAtAction(nameof(Add), new {id = id.GetGuid()}, id.GetGuid().ToString());
		}

		[HttpPatch("{id:Guid}")]
		[ProducesResponseType((int) HttpStatusCode.NoContent)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditGameRequest request)
		{
			var command = new EditGameCommand(
				id,
				request.Title,
				request.MaxPlayers,
				request.MinPlayers,
				request.HasScores);

			await _sender.Send(command);

			return NoContent();
		}

		[HttpDelete("{id:Guid}")]
		[ProducesResponseType((int) HttpStatusCode.NoContent)]
		[ProducesResponseType((int) HttpStatusCode.NotFound)]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			await _sender.Send(new DeleteGameCommand(id));

			return NoContent();
		}

		[HttpHead]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public async Task<IActionResult> IsTitleUnique(string title, Guid? id)
		{
			var isUnique = await _sender.Send(new IsTitleUniqueQuery(title, id));

			if (!isUnique)
			{
				return Conflict("Title is not unique.");
			}

			return Ok();
		}
	}
}