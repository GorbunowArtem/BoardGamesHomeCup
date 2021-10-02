using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation
{
	[ExcludeFromCodeCoverage]
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
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

					config.AddJsonFile("appsettings.json");
				})
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}