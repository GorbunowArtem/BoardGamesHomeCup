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
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Revo.AspNetCore;
using Revo.AspNetCore.Configuration;
using Revo.Core.Configuration;
using Revo.EFCore.Configuration;
using Revo.EFCore.DataAccess.Configuration;
using Revo.EFCore.DataAccess.Conventions;

namespace HorCup.Games
{
	public class Startup : RevoStartup
	{
		public Startup(IConfiguration configuration) : base(configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public override void ConfigureServices(IServiceCollection services)
		{
			base.ConfigureServices(services);
			
			services.AddDbContext<GamesContext>(options =>
				options.UseSqlServer(Configuration["ConnectionString"],
					sqlOptions => { sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null); }));

			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HorCup", Version = "v1" }); });

			services.AddHealthChecks();

			services.AddInfrastructure();

			services.AddScoped<IGamesContext, GamesContext>();
			services.AddScoped<IGamesService, GamesService>();
		}

		public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			base.Configure(app, env, loggerFactory);
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

		protected override IRevoConfiguration CreateRevoConfiguration() =>
			new RevoConfiguration()
				.UseAspNetCore()
				.UseEFCoreDataAccess(contextBuilder =>
						contextBuilder.UseSqlServer(Configuration["ConnectionString"]),
					advancedAction: config =>
					{
						config.AddConvention<BaseTypeAttributeConvention>(-200)
							.AddConvention<IdColumnsPrefixedWithTableNameConvention>(-110)
							.AddConvention<PrefixConvention>(-9)
							.AddConvention<SnakeCaseTableNamesConvention>(1);
					})
				.UseAllEFCoreInfrastructure();
	}
}