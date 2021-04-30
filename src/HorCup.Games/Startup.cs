using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using HorCup.Games.Commands.AddGame;
using HorCup.Games.Context;
using HorCup.Games.Services.Games;
using HorCup.Infrastructure.Filters;
using HorCup.Infrastructure.Services.DateTimeService;
using HorCup.Infrastructure.Services.IdGenerator;
using MediatR;
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
				options.UseSqlServer(Configuration.GetConnectionString("GamesContext")));
			
			services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilter)))
				.AddFluentValidation(v => { v.RegisterValidatorsFromAssemblyContaining<AddGameCommandValidator>(); })
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
				});

			services.AddMediatR(Assembly.GetAssembly(typeof(AddGameCommand)));
			services.AddAutoMapper(Assembly.GetAssembly(typeof(AddGameCommand)));

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo {Title = "HorCup.Games", Version = "v1"});
			});
			
			services.AddScoped<IGamesContext, GamesContext>();
			services.AddScoped<IGamesService, GamesService>();
			services.AddScoped<IIdGenerator, IdGenerator>();
			services.AddScoped<IDateTimeService, DateTimeService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HorCup.Games v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}