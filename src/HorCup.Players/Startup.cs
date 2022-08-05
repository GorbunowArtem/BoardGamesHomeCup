using System;
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

namespace HorCup.Players;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<PlayersContext>(options =>
			options.UseSqlServer(Configuration["ConnectionString"],
				sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
				}));

		services.AddInfrastructure();
			
		services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HorCup", Version = "v1"}); });

		services.AddHealthChecks();
			
		services.AddScoped<IPlayersContext, PlayersContext>();
		services.AddScoped<IPlayersService, PlayersService>();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		app.UseCors("AllowAll");
			
		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HorCup.Players v1"));

		// app.UseHttpsRedirection();

		app.UseRouting();

		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapHealthChecks("/health");
			endpoints.MapControllers();
		});
	}
}