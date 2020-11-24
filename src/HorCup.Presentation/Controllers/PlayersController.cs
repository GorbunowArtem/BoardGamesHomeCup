using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Players;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Presentation.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers
{
	[ApiController]
	[Route("players")]
	public class PlayersController: ControllerBase
	{
		private readonly ISender _sender;

		public PlayersController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		public async Task<ActionResult<PlayerViewModel>> Add(AddPlayerCommand commandHandler)
		{
			var player = await _sender.Send(commandHandler);

			return CreatedAtAction(nameof(Add), new {id = player.Id}, player);
		}

		[HttpGet("constraints")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public ActionResult<PlayerConstraints> GetConstraints()
		{
			return Ok(new PlayerConstraints());
		}
	}
}