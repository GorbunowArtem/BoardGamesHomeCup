using HorCup.Players.Models;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Players.Controllers
{
	[ApiController]
	[Route("players/constraints")]
	public class ConstraintsController : ControllerBase
	{
		[HttpGet]
		public ActionResult<PlayerConstraints> Get() => new(new PlayerConstraints());
	}
}