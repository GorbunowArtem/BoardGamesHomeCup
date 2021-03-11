using IdentityServer4.Models;
using System.Collections.Generic;

namespace HorCup.IdentityServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> IdentityResources =>
			new IdentityResource[]
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};

		public static IEnumerable<ApiScope> ApiScopes =>
			new ApiScope[]
			{
				new("scope1"),
				new("scope2"),
				new("api.read"),
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
				// interactive client using code flow + pkce
				new()
				{
					ClientId = "HorCup.PKCE",
					ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},

					AllowedGrantTypes = GrantTypes.Code,

					RedirectUris = {"https://localhost:5002/signin-oidc"},
					FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
					PostLogoutRedirectUris = {"https://localhost:5002/signout-callback-oidc"},

					AllowOfflineAccess = true,
					AllowedScopes = {"openid", "profile"}
				},
				new()
				{
					ClientId = "HorCup.SPA",
					ClientName = "HorCup SPA",
					AllowedGrantTypes = GrantTypes.Implicit,
					RequireClientSecret = false,
					AllowedScopes = {"openid", "profile"},
					RedirectUris = {"https://localhost:5002/auth-callback"},
					PostLogoutRedirectUris = {"https://localhost:5002/"},
					AllowedCorsOrigins = {"https://localhost:5002/"},
					AllowAccessTokensViaBrowser = true,
					AccessTokenLifetime = 3600
				},
				new()
				{
					ClientId = "angular_spa",
					ClientName = "Angular SPA",
					AllowedGrantTypes = GrantTypes.Implicit,
					AllowedScopes = {"openid", "profile"},
					RedirectUris = {"https://localhost:5002/auth-callback"},
					PostLogoutRedirectUris = {"https://localhost:5002"},
					
					AllowedCorsOrigins = {"https://localhost:5002"},
					AllowAccessTokensViaBrowser = true,
					AccessTokenLifetime = 3600
				}
			};
	}
}