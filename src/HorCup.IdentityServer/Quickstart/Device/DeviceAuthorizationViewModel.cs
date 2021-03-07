using HorCup.IdentityServer.Quickstart.Consent;
using IdentityServerHost.Quickstart.UI;

namespace HorCup.IdentityServer.Quickstart.Device
{
	public class DeviceAuthorizationViewModel : ConsentViewModel
	{
		public string UserCode { get; set; }
		public bool ConfirmUserCode { get; set; }
	}
}