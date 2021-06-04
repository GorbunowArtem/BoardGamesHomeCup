using HorCup.Infrastructure;
using HorCup.Statistic.Context;
using HorCup.Statistic.GamesStatistic.Events.GamePlayed;
using HorCup.Statistic.PlayersStatistic.Events;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HorCup.Statistic
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
			services.AddDbContext<StatisticContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("StatisticContext")));

			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HorCup", Version = "v1"}); });

			services.AddInfrastructure();

			services.AddMassTransit(configuration =>
			{
				configuration.AddConsumer<GamePlayedEventConsumer>();
				configuration.AddConsumer<PlayerPlayedEventConsumer>();

				configuration.SetKebabCaseEndpointNameFormatter();

				configuration.UsingRabbitMq((context, cfg) =>
				{
					cfg.ConfigureEndpoints(context);
				});
			});

			services.AddHealthChecks();
			
			services.AddMassTransitHostedService();
			
			services.AddScoped<IStatisticContext, StatisticContext>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HorCup.Statistic v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/health");
				endpoints.MapControllers();
			});
		}
	}
}