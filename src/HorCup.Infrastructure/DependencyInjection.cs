using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using HorCup.Infrastructure.EventBus;
using HorCup.Infrastructure.EventBus.RabbitMQEventBus;
using HorCup.Infrastructure.Filters;
using HorCup.Infrastructure.Services.DateTimeService;
using HorCup.Infrastructure.Services.IdGenerator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HorCup.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilter)))
				.AddFluentValidation(v => { v.RegisterValidatorsFromAssembly(Assembly.GetCallingAssembly()); })
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
				});

			// services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HorCup", Version = "v1"}); });

			services.AddMediatR(Assembly.GetCallingAssembly());
			services.AddAutoMapper(Assembly.GetCallingAssembly());

			services.AddTransient<IIdGenerator, IdGenerator>();
			services.AddTransient<IDateTimeService, DateTimeService>();

			services.AddSingleton<IEventBus, RabbitEventBus>();
			
			services.AddCors(options => options.AddPolicy("AllowAll", p =>
			{
				p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
			}));
			
			return services;
		}
	}
}                                                               