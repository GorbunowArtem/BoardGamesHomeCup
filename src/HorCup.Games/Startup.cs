using System;
using System.IO;
using Akka.Actor;
using Akka.Configuration;
using Akka.Persistence.MongoDb;
using HorCup.Games.Models;
using HorCup.Games.Queries;
using HorCup.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HorCup.Games
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HorCup", Version = "v1" }); });

			services.AddHealthChecks();

			services.AddInfrastructure();

			var path = Environment.CurrentDirectory;
			var configPath = Path.Combine(path, "akka.conf");
			var config = ConfigurationFactory.ParseString(File.ReadAllText(configPath));
			var actorSystem = ActorSystem.Create("games", config);

			MongoDbPersistence.Get(actorSystem);
			
			var gamesActor = actorSystem.ActorOf(Props.Create(() => new GameManager()), "game-manager");
			var gamesStorage =
				actorSystem.ActorOf(Props.Create(() => new GameStorageHandler()), "game-storage-handler");

			services.AddAkkatecture(actorSystem)
				.AddActorReference<GameManager>(gamesActor)
				.AddActorReference<GameStorageHandler>(gamesStorage);


			services.AddTransient<IGameActorService, GameActorService>();
			services.AddTransient<IGamesQueryHandler, GamesQueryHandler>();

			// services.AddScoped<IGamesService, GamesService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors("AllowAll");

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HorCup.Games v1"));

			// app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHealthChecks("/health");
			});
		}
	}
}