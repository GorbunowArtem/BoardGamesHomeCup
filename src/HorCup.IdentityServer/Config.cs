﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


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
			};

		public static IEnumerable<Client> Clients =>
			new Client[]
			{
				// m2m client credentials flow client
				new()
				{
					ClientId = "m2m.client",
					ClientName = "Client Credentials Client",

					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},

					AllowedScopes = {"scope1"}
				},

				// interactive client using code flow + pkce
				new()
				{
					ClientId = "interactive",
					ClientSecrets = {new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256())},

					AllowedGrantTypes = GrantTypes.Code,

					RedirectUris = {"https://localhost:44300/signin-oidc"},
					FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
					PostLogoutRedirectUris = {"https://localhost:44300/signout-callback-oidc"},

					AllowOfflineAccess = true,
					AllowedScopes = {"openid", "profile", "scope2"}
				},
				new()
				{
					ClientId = "angular_spa",
					ClientName = "Angular SPA",
					AllowedGrantTypes = GrantTypes.Implicit,
					AllowedScopes = {"openid", "profile", "email", "api.read"},
					RedirectUris = {"http://localhost:4200/auth-callback"},
					PostLogoutRedirectUris = {"http://localhost:4200/"},
					AllowedCorsOrigins = {"http://localhost:4200"},
					AllowAccessTokensViaBrowser = true,
					AccessTokenLifetime = 3600
				}
			};
	}
}