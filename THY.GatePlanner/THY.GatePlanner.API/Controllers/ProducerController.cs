using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using THY.GatePlanner.API.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace THY.GatePlanner.API.Controllers
{
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IHubContext<PlaneGateHub> _hub;

        public ProducerController(IHubContext<PlaneGateHub> hub)
        {
            _hub = hub;
        }
        [HttpGet("Produce")]
        public async void Get()
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", "deneme");

        } 
    }
}
