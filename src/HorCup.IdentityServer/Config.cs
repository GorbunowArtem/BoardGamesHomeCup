using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

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
				new("api.admin"),
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
				new()
				{
					ClientId = "HorCup.PKCE",
					ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},
					AllowedGrantTypes = GrantTypes.Code,
					RedirectUris = {"https://localhost:5002/signin-oidc"},
					FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
					PostLogoutRedirectUris = {"https://localhost:5002/signout-callback-oidc"},
					AllowOfflineAccess = true,
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"api.admin"
					}
				},
				new()
				{
					ClientId = "HorCup.SPA",
					ClientName = "HorCup SPA",
					RequireConsent = false,
					RequireClientSecret = false,
					AllowAccessTokensViaBrowser = true,
					AllowOfflineAccess = true,
					AllowedGrantTypes = GrantTypes.Code,
					RequirePkce = true,
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"api.admin"
					},
					RedirectUris = {"https://localhost:5002/auth-callback"},
					PostLogoutRedirectUris = {"https://localhost:5002"},
					AllowedCorsOrigins = {"https://localhost:5002"},
					AccessTokenLifetime = 3600
				}
			};
	}
}