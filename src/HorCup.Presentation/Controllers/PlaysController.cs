using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Plays.Commands.AddPlay;
using HorCup.Presentation.Plays.Queries.SearchPlays;
using HorCup.Presentation.Responses;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers
{
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

			return Ok(new PagedSearchResponse<PlayViewModel>(items, total));
		}

		[HttpPost]
		[ProducesResponseType((int) HttpStatusCode.Created)]
		[ProducesResponseType((int) HttpStatusCode.Conflict)]
		public async Task<ActionResult<PlayViewModel>> Add([FromBody] AddPlayCommand command)
		{
			var playId = await _sender.Send(command);

			return CreatedAtAction(nameof(Add), new {id = playId});
		}
	}
}