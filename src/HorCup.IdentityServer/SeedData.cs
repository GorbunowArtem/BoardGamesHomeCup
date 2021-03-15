using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using IdentityServer4withASP.NETCoreIdentity.Data;
using IdentityServer4withASP.NETCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace HorCup.IdentityServer
{
	public class SeedData
	{
		public static void EnsureSeedData(string connectionString)
		{
			var services = new ServiceCollection();
			services.AddLogging();
			
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			using var serviceProvider = services.BuildServiceProvider();
			using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

			context.Database.Migrate();

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
				var result = userMgr.CreateAsync(artem, "Pass123$").Result;
				if (!result.Succeeded)
				{
					throw new Exception(result.Errors.First().Description);
				}

				context.SaveChanges();
			}
		}


		public static void EnsureSeedClients(string connectionString)
		{
			var services = new ServiceCollection();
			services.AddOperationalDbContext(options =>
			{
				options.ConfigureDbContext = db => db.UseSqlServer(connectionString,
					sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
			});
			services.AddConfigurationDbContext(options =>
			{
				options.ConfigureDbContext = db => db.UseSqlServer(connectionString,
					sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
			});

			var serviceProvider = services.BuildServiceProvider();

			using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
			scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

			var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
			context.Database.Migrate();
			EnsureSeedData(context);
		}

		private static void EnsureSeedData(ConfigurationDbContext context)
		{
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
	}
}