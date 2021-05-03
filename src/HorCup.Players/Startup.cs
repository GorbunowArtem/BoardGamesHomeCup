using HorCup.Infrastructure;
using HorCup.Players.Context;
using HorCup.Players.Services.Players;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HorCup.Players
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
			var dbConfig = Configuration.GetSection("CosmosDb");
			
			services.AddDbContext<PlayersContext>(options =>
				options.UseCosmos(dbConfig["AccountEndpoint"],
					dbConfig["AccountKey"],
					dbConfig["DatabaseName"]));
			
			services.AddInfrastructure();
			
			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HorCup", Version = "v1"}); });

			services.AddScoped<IPlayersContext, PlayersContext>();
			services.AddScoped<IPlayersService, PlayersService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HorCup.Players v1"));

			// app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}