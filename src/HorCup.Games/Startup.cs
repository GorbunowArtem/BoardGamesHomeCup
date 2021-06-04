using System;
using HorCup.Games.Context;
using HorCup.Games.Services.Games;
using HorCup.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
			services.AddDbContext<GamesContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("GamesContext"),
					sqlOptions =>
					{
						sqlOptions.EnableRetryOnFailure(30, TimeSpan.FromSeconds(30), null);
					}));

			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HorCup", Version = "v1"}); });

			services.AddHealthChecks();
			
			services.AddInfrastructure();
			
			services.AddScoped<IGamesContext, GamesContext>();
			services.AddScoped<IGamesService, GamesService>();
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