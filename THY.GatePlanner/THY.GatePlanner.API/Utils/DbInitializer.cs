using System;
using THY.GatePlanner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace THY.GatePlanner.API.Utils
{
    public static class DbInitializer
    {
        public static void InitializeDb(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<GatePlannerContext>();
                dataContext.Database.Migrate();
            }
        }
    }
}

