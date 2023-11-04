using System;
using System.Net;
using System.Text.Json;

namespace THY.GatePlanner.API.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Middleware catched");

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var json = JsonSerializer.Serialize(
                    $"{{Error: {context.Request.RouteValues["controller"]}/{context.Request.RouteValues["action"]}}}");

                await context.Response.WriteAsync(json);
            }
        }
    }
}

