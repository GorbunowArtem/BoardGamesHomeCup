using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using HorCup.Infrastructure.Responses;
using HorCup.Plays.Commands.AddPlay;
using HorCup.Plays.Queries.SearchPlays;
using HorCup.Plays.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Plays.Controllers
{
	[ExcludeFromCodeCoverage]
	[ApiController]
	[Route("plays")]
	public class PlaysController : ControllerBase
	{
		private readonly ISender _sender;

		public PlaysController(ISender sender)
		{
			_sender = sender;
		}

		[HttpGet]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		public async Task<ActionResult<PagedSearchResponse<PlayViewModel>>> Search([FromQuery] SearchPlaysQuery query)
		{
			var (items, total) = await _sender.Send(query);
																			  
			return Ok(new PagedSearchResponse<PlayViewModel>(items, (int)total));
		}

		[HttpPost]
		[ProducesResponseType((int) HttpStatusCode.Created)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public async Task<ActionResult<Guid>> Add([FromBody] AddPlayCommand command)
		{
			var id = await _sender.Send(command);

			return CreatedAtAction(nameof(Add), new {id}, id.ToString());
		}
	}
}