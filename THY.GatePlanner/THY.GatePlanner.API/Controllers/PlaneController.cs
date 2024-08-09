using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using System.Net;
using System.Text.Json;
using THY.GatePlanner.API.Hubs;
using THY.GatePlanner.API.RabbitMQ;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;
using THY.GatePlanner.Service.GateService;
using THY.GatePlanner.Service.PlaneService;

namespace THY.GatePlanner.API.Controllers
{

    public class PlaneController : ControllerBase
    {
        private readonly IPlaneService _planeService;
        private readonly IHubContext<PlaneGateHub> _hub;
        private readonly IRabbitMqService _rabbitMq;

        public PlaneController(IPlaneService planeService, IHubContext<PlaneGateHub> hub, IRabbitMqService rabbitMQ)
        {
            _planeService = planeService;
            _hub = hub;
            _rabbitMq = rabbitMQ;
        }

        [HttpGet("Plane")]
        public async Task<ActionResult<GetPlanesResponse>> GetPlanes([FromQuery] GetPlanesRequest getPlanesRequest)
        {
            var serviceResult = await _planeService.GetPlanesAsync(getPlanesRequest);

            var response = serviceResult;

            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpPost("Plane")]
        public async Task<ActionResult<CreatePlaneResponse>> CreatePlane([FromBody] CreatePlaneRequest createPlaneRequest)
        {
            var serviceResult = await _planeService.CreatePlaneAsync(createPlaneRequest);

            _rabbitMq.SendMessage("plane", JsonSerializer.Serialize(new { serviceResult.Id, createPlaneRequest.Code }));

            await _hub.Clients.All.SendAsync("PlaneQueued", new { code = createPlaneRequest.Code, size = createPlaneRequest.Size });

            return StatusCode((int)HttpStatusCode.OK, serviceResult);
        }
    }
}
