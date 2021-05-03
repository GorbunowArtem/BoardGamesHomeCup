using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using HorCup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HorCup.Infrastructure.Filters
{
	public class CustomExceptionFilter : IAsyncExceptionFilter
	{
		private readonly ILogger<CustomExceptionFilter> _logger;

		public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
		{
			_logger = logger;
		}

		public async Task OnExceptionAsync(ExceptionContext context)
		{
			var statusCode = HttpStatusCode.InternalServerError;
			var message = context.Exception.Message;
			
			switch (context.Exception)
			{
				case ValidationException validationException:
					statusCode = HttpStatusCode.BadRequest;
					message = JsonSerializer.Serialize(validationException.Failures);
					break;
				case NotFoundException:
					statusCode = HttpStatusCode.NotFound;
					break;
				case EntityExistsException:
					statusCode = HttpStatusCode.Conflict;
					break;
			}

			_logger.LogError(message);
			
			context.ExceptionHandled = true;

			var response = context.HttpContext.Response;
			response.StatusCode = (int) statusCode;

			await response.WriteAsync(message);
		}
	}
}
