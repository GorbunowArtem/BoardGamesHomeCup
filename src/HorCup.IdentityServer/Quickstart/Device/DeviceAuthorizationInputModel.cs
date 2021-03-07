using HorCup.IdentityServer.Quickstart.Consent;
using IdentityServerHost.Quickstart.UI;

namespace HorCup.IdentityServer.Quickstart.Device
{
	public class DeviceAuthorizationInputModel : ConsentInputModel
	{
		public string UserCode { get; set; }
	}
}