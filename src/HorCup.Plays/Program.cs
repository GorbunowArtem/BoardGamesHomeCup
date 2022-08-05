using System;
using HorCup.Plays.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HorCup.Plays;

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
			.ConfigureAppConfiguration((hostingContext, config) =>
			{
				var env = hostingContext.HostingEnvironment;

				config.AddJsonFile("appsettings.json");
			})
			.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

	private static void CreateDbIfNotExists(IHost host)
	{
		using var scope = host.Services.CreateScope();
		var services = scope.ServiceProvider;
		try
		{
			var context = services.GetRequiredService<IPlaysContext>();

#if DEBUG
			DbInitializer.Initialize(context);
				
#endif
		}
		catch (Exception ex)
		{
			var logger = services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred creating the DB.");
		}
	}
}