
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Writers;
using System.Text.Json;
using THY.GatePlanner.API.Controllers;
using THY.GatePlanner.API.Hubs;
using THY.GatePlanner.API.RabbitMQ;
using THY.GatePlanner.Model.Enums;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;
using THY.GatePlanner.Service.GateService;
using THY.GatePlanner.Service.PlaneGateService;
using THY.GatePlanner.Service.PlaneService;

namespace THY.GatePlanner.API
{
    public class QueueConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QueueConsumerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _gateService = scope.ServiceProvider.GetService<IGateService>()!;
                        var _rabbitMQService = scope.ServiceProvider.GetService<IRabbitMqService>()!;
                        var _planeGateService = scope.ServiceProvider.GetService<IPlaneGateService>()!;
                        var _hubContext = scope.ServiceProvider.GetService<IHubContext<PlaneGateHub>>()!;

                        var isThereAvailableGate = await _gateService.IsThereAvailableGateAsync();

                        if (!isThereAvailableGate.IsThereAvailableGate)
                        {
                            await Task.Delay(5000, stoppingToken);
                            continue;
                        }

                        ulong outDeliveryTag;
                        var message = _rabbitMQService.BasicGet("plane", out outDeliveryTag);
                        if (!string.IsNullOrEmpty(message))
                        {
                            JsonDocument document = JsonDocument.Parse(message);
                            JsonElement root = document.RootElement;

                            string planeId = root.GetProperty("Id").GetString()!;
                            string planeCode = root.GetProperty("Code").GetString()!;

                            var assignResult = await _planeGateService.AssignGateToPlane(new AssignGateToPlaneRequest() { PlaneId = Guid.Parse(planeId) });

                            _rabbitMQService.Acknowledge(outDeliveryTag);
                            if (assignResult.PlaneId != default)
                            {
                                await _hubContext.Clients.All.SendAsync("PlaneAssigned", new { gateId = assignResult.GateId, planeId = assignResult.PlaneId }); 
                            } 
                            else 
                                _rabbitMQService.SendMessage("plane", JsonSerializer.Serialize(new { Id=planeId,Code= planeCode }));
                        }
                        await Task.Delay(1000, stoppingToken);

                    }
                }
                catch (Exception ex)
                {

                    throw;
                }


            }
        }
    }
}
