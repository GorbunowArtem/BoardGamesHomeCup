using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Players;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Presentation.Services.Players;
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
		private readonly IPlayersService _playersService;

		public PlayersController(ISender sender, IPlayersService playersService)
		{
			_sender = sender;
			_playersService = playersService;
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Conflict)]
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

		[HttpHead]
		public async Task<IActionResult> IsNicknameUnique(string nickname)
		{
			var isUnique = await _playersService.IsNicknameUniqueAsync(nickname);
			
			if (isUnique)
			{
				return Ok();
			}

			return Conflict();
		}
	}
}