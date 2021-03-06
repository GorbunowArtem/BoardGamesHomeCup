﻿using System;
using System.Linq;
using HorCup.IdentityServer.Data;
using HorCup.IdentityServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HorCup.IdentityServer.Extensions
{
	public static class SeedData
	{
		public static void MigrateAndSeedDb(this IApplicationBuilder builder, IConfiguration configuration)
		{
			using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

			MigrateAndSeedApplicationDb(configuration, scope);

			MigrateAndSeedPersistedGrantDb(scope);

			MigrateAndSeedConfigurationDb(scope);
		}

		private static void MigrateAndSeedConfigurationDb(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
			context.Database.Migrate();

			if (!context.Clients.Any())
			{
				Log.Debug("Clients being populated");
				foreach (var client in Config.Clients.ToList())
				{
					context.Clients.Add(client.ToEntity());
				}

				context.SaveChanges();

				Log.Debug("IdentityResources being populated");
				foreach (var resource in Config.IdentityResources.ToList())
				{
					context.IdentityResources.Add(resource.ToEntity());
				}

				context.SaveChanges();

				Log.Debug("ApiScopes being populated");
				foreach (var resource in Config.ApiScopes.ToList())
				{
					context.ApiScopes.Add(resource.ToEntity());
				}

				context.SaveChanges();
			}
		}

		private static void MigrateAndSeedPersistedGrantDb(IServiceScope scope)
		{
			scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
		}

		private static void MigrateAndSeedApplicationDb(IConfiguration configuration, IServiceScope scope)
		{
			var appContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

			appContext.Database.Migrate();

			var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			var artem = userMgr.FindByNameAsync("artem").Result;
			if (artem == null)
			{
				artem = new ApplicationUser
				{
					Id = Guid.NewGuid().ToString(),
					UserName = "artem",
					Email = "artem@email.com",
					EmailConfirmed = true
				};

				var result = userMgr.CreateAsync(artem, configuration["DefaultUser:Password"]).Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}

				appContext.SaveChanges();
			}
		}
	}
}