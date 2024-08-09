
using Microsoft.AspNetCore.SignalR;
using THY.GatePlanner.API.Controllers;
using THY.GatePlanner.API.Hubs;
using THY.GatePlanner.API.RabbitMQ;
using THY.GatePlanner.Model.Enums;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Service.PlaneService;

namespace THY.GatePlanner.API
{
    public class QueueConsumerService : BackgroundService
    {

        private readonly IRabbitMqService _rabbitMqService;
        private readonly IHubContext<PlaneGateHub> _hubContext;

        public QueueConsumerService(IRabbitMqService rabbitMQService, IHubContext<PlaneGateHub> hubContext)
        {
            _rabbitMqService = rabbitMQService;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _rabbitMqService.StartConsuming("plane", async message =>
                {
                    // service call
                  await  _hubContext.Clients.All.SendAsync("PlaneAssigned", new { code = message });
                });

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
