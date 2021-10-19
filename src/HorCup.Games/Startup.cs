using System.Linq;
using System.Reflection;
using CQRSlite.Caching;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using CQRSlite.Messages;
using CQRSlite.Queries;
using CQRSlite.Routing;
using HorCup.Games.Commands;
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
			services.AddSingleton<Router>(new Router());
			services.AddSingleton<ICommandSender>(y => y.GetService<Router>());
			services.AddSingleton<IEventPublisher>(y => y.GetService<Router>());
			services.AddSingleton<IHandlerRegistrar>(y => y.GetService<Router>());
			services.AddSingleton<IQueryProcessor>(y => y.GetService<Router>());
			services.AddSingleton<IEventStore, InMemoryEventStore>();
			services.AddSingleton<ICache, MemoryCache>();
			services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()),
				y.GetService<IEventStore>(), y.GetService<ICache>()));
			services.AddScoped<ISession, Session>();

			//Scan for commandhandlers and eventhandlers
			services.Scan(scan => scan
				.FromAssemblies(typeof(GameCommandHandler).GetTypeInfo().Assembly)
				.AddClasses(classes => classes.Where(x =>
				{
					var allInterfaces = x.GetInterfaces();
					return
						allInterfaces.Any(y =>
							y.GetTypeInfo().IsGenericType &&
							y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IHandler<>)) ||
						allInterfaces.Any(y =>
							y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() ==
							typeof(ICancellableHandler<>)) ||
						allInterfaces.Any(y =>
							y.GetTypeInfo().IsGenericType &&
							y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IQueryHandler<,>)) ||
						allInterfaces.Any(y =>
							y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() ==
							typeof(ICancellableQueryHandler<,>));
				}))
				.AsSelf()
				.WithTransientLifetime()
			);

			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HorCup", Version = "v1" }); });

			services.AddHealthChecks();

			services.AddInfrastructure();
		}

		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env)
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