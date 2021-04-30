using System;
using System.Diagnostics.CodeAnalysis;
using HorCup.Presentation.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation
{
	[ExcludeFromCodeCoverage]
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			CreateDbIfNotExists(host);

			host.Run();
		}

		private static void CreateDbIfNotExists(IHost host)
		{
			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;
			try
			{
				var context = services.GetRequiredService<HorCupContext>();
				context.Database.Migrate();
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

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
				})
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					var env = hostingContext.HostingEnvironment;

					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
						.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
				})
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}