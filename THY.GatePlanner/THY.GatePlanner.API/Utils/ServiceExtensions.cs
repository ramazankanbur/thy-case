using System;
using System.ComponentModel.Design;
using THY.GatePlanner.Infrastructure.Persistence;
using THY.GatePlanner.Infrastructure.Persistence.Repositories;
using THY.GatePlanner.Infrastructure.Persistence.UOW;
using THY.GatePlanner.Service.GateService;
using Microsoft.EntityFrameworkCore;
using THY.GatePlanner.API.RabbitMQ;
using THY.GatePlanner.Service.PlaneService;

namespace THY.GatePlanner.API.Utils
{
	internal static class ServiceExtensions
	{
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGateService, GateService>();
            services.AddScoped<IPlaneService, PlaneService>();

        }

        public static void AddDataLayer(this WebApplicationBuilder builder)
        {
            var connString = builder.Configuration.GetConnectionString("MSSqlConnection");
            builder.Services.AddDbContextPool<GatePlannerContext>(
                options => options.UseSqlServer(connString));
        }
    }
}

