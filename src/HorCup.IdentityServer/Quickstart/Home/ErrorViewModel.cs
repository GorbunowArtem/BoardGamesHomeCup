using IdentityServer4.Models;

namespace HorCup.IdentityServer.Quickstart.Home
{
	public class ErrorViewModel
	{
		public ErrorViewModel()
		{
		}

		public ErrorViewModel(string error)
		{
			Error = new ErrorMessage {Error = error};
		}

		public ErrorMessage Error { get; set; }
	}
}