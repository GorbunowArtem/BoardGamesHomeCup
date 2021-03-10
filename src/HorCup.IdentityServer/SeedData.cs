using System;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
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

			using (var serviceProvider = services.BuildServiceProvider())
			{
				using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
				{
					var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
					context.Database.Migrate();

					var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
					var alice = userMgr.FindByNameAsync("alice").Result;
					if (alice == null)
					{
						alice = new ApplicationUser
						{
							Id = Guid.NewGuid().ToString(),
							UserName = "alice",
							Email = "AliceSmith@email.com",
							EmailConfirmed = true,
						};
						var result = userMgr.CreateAsync(alice, "Pass123$").Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}

						result = userMgr.AddClaimsAsync(alice, new Claim[]
							{
								new Claim(JwtClaimTypes.Name, "Alice Smith"),
								new Claim(JwtClaimTypes.GivenName, "Alice"),
								new Claim(JwtClaimTypes.FamilyName, "Smith"),
								new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
							})
							.Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}

						Log.Debug("alice created");
					}
					else
					{
						Log.Debug("alice already exists");
					}

					var bob = userMgr.FindByNameAsync("bob").Result;
					if (bob == null)
					{
						bob = new ApplicationUser
						{
							Id = Guid.NewGuid().ToString(),
							UserName = "bob",
							Email = "BobSmith@email.com",
							EmailConfirmed = true
						};
						var result = userMgr.CreateAsync(bob, "Pass123$").Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}

						result = userMgr.AddClaimsAsync(bob, new Claim[]
							{
								new Claim(JwtClaimTypes.Name, "Bob Smith"),
								new Claim(JwtClaimTypes.GivenName, "Bob"),
								new Claim(JwtClaimTypes.FamilyName, "Smith"),
								new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
								new Claim("location", "somewhere")
							})
							.Result;
						if (!result.Succeeded)
						{
							throw new Exception(result.Errors.First().Description);
						}

						Log.Debug("bob created");
					}
					else
					{
						Log.Debug("bob already exists");
					}
				}
			}
		}
	}
	// public class SeedData
	// {
	// 	public static void EnsureSeedData(string connectionString)
	// 	{
	// 		var services = new ServiceCollection();
	// 		services.AddOperationalDbContext(options =>
	// 		{
	// 			options.ConfigureDbContext = db => db.UseSqlServer(connectionString,
	// 				sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
	// 		});
	// 		services.AddConfigurationDbContext(options =>
	// 		{
	// 			options.ConfigureDbContext = db => db.UseSqlServer(connectionString,
	// 				sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
	// 		});
	//
	// 		var serviceProvider = services.BuildServiceProvider();
	//
	// 		using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
	// 		{
	// 			scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
	//
	// 			var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
	// 			context.Database.Migrate();
	// 			EnsureSeedData(context);
	// 		}
	// 	}
	//
	// 	private static void EnsureSeedData(ConfigurationDbContext context)
	// 	{
	// 		if (!context.Clients.Any())
	// 		{
	// 			Log.Debug("Clients being populated");
	// 			foreach (var client in Config.Clients.ToList())
	// 			{
	// 				context.Clients.Add(client.ToEntity());
	// 			}
	//
	// 			context.SaveChanges();
	// 		}
	// 		else
	// 		{
	// 			Log.Debug("Clients already populated");
	// 		}
	//
	// 		if (!context.IdentityResources.Any())
	// 		{
	// 			Log.Debug("IdentityResources being populated");
	// 			foreach (var resource in Config.IdentityResources.ToList())
	// 			{
	// 				context.IdentityResources.Add(resource.ToEntity());
	// 			}
	//
	// 			context.SaveChanges();
	// 		}
	// 		else
	// 		{
	// 			Log.Debug("IdentityResources already populated");
	// 		}
	//
	// 		if (!context.ApiResources.Any())
	// 		{
	// 			Log.Debug("ApiScopes being populated");
	// 			foreach (var resource in Config.ApiScopes.ToList())
	// 			{
	// 				context.ApiScopes.Add(resource.ToEntity());
	// 			}
	//
	// 			context.SaveChanges();
	// 		}
	// 		else
	// 		{
	// 			Log.Debug("ApiScopes already populated");
	// 		}
	// 	}
	// }
}