using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using HorCup.Infrastructure.Responses;
using HorCup.Presentation.PlayersStatistic.Queries.SearchPlayerStats;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers
{
	[ExcludeFromCodeCoverage]
	[ApiController]
	[Route("players-statistic")]
	public class PlayerStatisticController : ControllerBase
	{
		private readonly ISender _sender;

		public PlayerStatisticController(ISender sender)
		{
			_sender = sender;
		}

		[HttpGet]
		[ProducesResponseType((int) HttpStatusCode.OK)]
		public async Task<ActionResult<PagedSearchResponse<PlayerStatisticViewModel>>> Search(
			[FromQuery] SearchPlayerStatsQuery query)
		{
			var (items, total) = await _sender.Send(query);

			return Ok(new PagedSearchResponse<PlayerStatisticViewModel>(items, total));
		}
	}
}