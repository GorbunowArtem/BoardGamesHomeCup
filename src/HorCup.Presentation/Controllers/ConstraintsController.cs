using System.Diagnostics.CodeAnalysis;
using System.Net;
using HorCup.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HorCup.Presentation.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("constraints")]
public class ConstraintsController: ControllerBase
{
	[HttpGet]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public ActionResult<ConstraintsViewModel> Get() =>
		Ok(new ConstraintsViewModel());
}