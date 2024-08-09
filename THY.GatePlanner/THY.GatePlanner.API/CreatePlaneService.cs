
using THY.GatePlanner.API.Controllers;
using THY.GatePlanner.Model.Enums;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Service.PlaneService;

namespace THY.GatePlanner.API
{
    public class CreatePlaneService : IHostedService, IDisposable
    {

        private Timer? _timer = null;

        private const int CreatePlaneInterval = 10;

        private readonly PlaneController _planeController;

        public CreatePlaneService(PlaneController planeController)
        {
            _planeController = planeController;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(CreatePlane, null, TimeSpan.Zero, TimeSpan.FromSeconds(CreatePlaneInterval));

            return Task.CompletedTask;
        }

        private async void CreatePlane(object? state)
        {
          await _planeController.CreatePlane(
                new CreatePlaneRequest()
                {
                    Code = "TK0111",
                    Size = SizeEnum.M.GetHashCode()
                });
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
