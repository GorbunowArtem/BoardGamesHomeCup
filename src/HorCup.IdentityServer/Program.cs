using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.Extensions.Logging;

namespace HorCup.IdentityServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			CreateLogger();
			
			CreateDbIfNotExists(host);

			host.Run();
		}

		private static void CreateLogger()
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
				.MinimumLevel.Override("System", LogEventLevel.Warning)
				.MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
				.Enrich.FromLogContext()
				// uncomment to write to Azure diagnostics stream
				//.WriteTo.File(
				//    @"D:\home\LogFiles\Application\identityserver.txt",
				//    fileSizeLimitBytes: 1_000_000,
				//    rollOnFileSizeLimit: true,
				//    shared: true,
				//    flushToDiskInterval: TimeSpan.FromSeconds(1))
				.WriteTo.Console(
					outputTemplate:
					"[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
					theme: AnsiConsoleTheme.Code)
				.CreateLogger();
		}

		private static void CreateDbIfNotExists(IHost host)
		{
			Log.Information("Seeding database...");
			using var scope = host.Services.CreateScope();
			
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
			
			var config = host.Services.GetRequiredService<IConfiguration>();
			var connectionString = config.GetConnectionString("DefaultConnection");
			
			SeedData.EnsureSeedData(connectionString);
			
			SeedData.EnsureSeedClients(connectionString);
			logger.LogInformation("Done seeding database.");
			logger.LogInformation("Starting host...");
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
				})
				.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}