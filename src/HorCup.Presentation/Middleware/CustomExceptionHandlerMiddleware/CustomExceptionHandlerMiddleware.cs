using System;
using System.Net;
using System.Threading.Tasks;
using HorCup.Presentation.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HorCup.Presentation.Middleware.CustomExceptionHandlerMiddleware
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
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

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
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

			if (result == string.Empty)
			{
				result = JsonConvert.SerializeObject(new {error = exception.Message});
			}

			return context.Response.WriteAsync(result);
		}
	}
}