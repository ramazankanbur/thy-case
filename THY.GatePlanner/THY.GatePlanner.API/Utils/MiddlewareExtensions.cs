using System;
using THY.GatePlanner.API.Middlewares;

namespace THY.GatePlanner.API.Utils
{
	public static class MiddlewareExtensions
	{
		 public static void AddMiddlewares(this WebApplication app)
		{
			app.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
}

