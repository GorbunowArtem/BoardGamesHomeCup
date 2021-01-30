using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HorCup.Presentation.Middleware.CustomExceptionHandlerMiddleware
{
	[ExcludeFromCodeCoverage]
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public CustomExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			_logger = loggerFactory.CreateLogger<CustomExceptionHandlerMiddleware>();
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var code = HttpStatusCode.InternalServerError;

			var result = string.Empty;

			switch (exception)
			{
				case ValidationException validationException:
					code = HttpStatusCode.BadRequest;
					result = JsonConvert.SerializeObject(validationException.Failures);
					break;
				case NotFoundException _:
					code = HttpStatusCode.NotFound;
					break;
				case EntityExistsException:
					code = HttpStatusCode.Conflict;
					break;
			}

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int) code;

			_logger.LogError(exception.Message);
			
			if (result == string.Empty)
			{
				result = JsonConvert.SerializeObject(new {error = exception.Message});
			}

			return context.Response.WriteAsync(result);
		}
	}
}