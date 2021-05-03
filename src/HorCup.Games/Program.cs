using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorCup.Games.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HorCup.Games
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			CreateDbIfNotExists(host);

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		
		private static void CreateDbIfNotExists(IHost host)
		{
 			using var scope = host.Services.CreateScope();
 			var services = scope.ServiceProvider;
 			try
 			{
 				var context = services.GetRequiredService<GamesContext>();
 				context.Database.EnsureCreated();

 #if DEBUG
 				// DbInitializer.Initialize(context);
 #endif
 			}
 			catch (Exception ex)
 			{
 				var logger = services.GetRequiredService<ILogger<Program>>();
 				logger.LogError(ex, "An error occurred creating the DB.");
 			}
		}

	}
}