using HorCup.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers
{
	[ApiController]
	[Route("constraints")]
	public class ConstraintsController: ControllerBase
	{
		public ActionResult<ConstraintsViewModel> Get()
		{
			return Ok(new ConstraintsViewModel());
		}
	}
}