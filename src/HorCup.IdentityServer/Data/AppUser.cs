using Microsoft.AspNetCore.Identity;

namespace HorCup.IdentityServer.Data
{
	public class AppUser: IdentityUser
	{
		public string Name { get; set; }
	}
}