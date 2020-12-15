﻿using System;
using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Games;
using HorCup.Presentation.Games.Commands.AddGame;
using HorCup.Presentation.Games.Queries.GetById;
using HorCup.Presentation.Games.Queries.SearchGames;
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
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult<PagedSearchResponse<GameViewModel>>> SearchGames([FromQuery]SearchGamesQuery query)
		{
			var (items, total) = await _sender.Send(query);

			return Ok(new PagedSearchResponse<GameViewModel>(items, total));
		}

		[HttpGet("{id:Guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<ActionResult<GameDetailsViewModel>> GetById(Guid id)
		{
			var game = await _sender.Send(new GetGameByIdQuery(id));

			return Ok(game);
		}

		[HttpPost]
		[ProducesResponseType((int) HttpStatusCode.Created)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public async Task<ActionResult<Guid>> Add([FromBody]AddGameCommand command)
		{
			var id = await _sender.Send(command);

			return CreatedAtAction(nameof(Add), id.ToString());
		}

		[HttpGet("constraints")]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		public ActionResult<GamesConstraints> GetConstraints() => Ok(new GamesConstraints());
	}
}