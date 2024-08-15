using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using THY.GatePlanner.API.Hubs;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;
using THY.GatePlanner.Service.GateService;

namespace THY.GatePlanner.API.Controllers
{
    public class GateController : ControllerBase
    {
        private readonly IGateService _gateService;
        private readonly IHubContext<PlaneGateHub> _hub;
        public GateController(IGateService gateService, IHubContext<PlaneGateHub> hub)
        {
            _gateService = gateService;
            _hub = hub;
        }

        [HttpGet("Gate")]
        public async Task<ActionResult<GetGatesResponse>> GetGates([FromQuery] GetGatesRequest getGatesRequest)
        {
            var serviceResult = await _gateService.GetGatesAsync(getGatesRequest);

            var response = serviceResult;            

            return StatusCode((int)HttpStatusCode.OK, response);
        }

        [HttpPost("Gate/MakeGateAvailable")]
        public async Task<ActionResult<MakeGateAvailableResponse>> MakeGateAvailable([FromBody] MakeGateAvailableRequest makeGateAvailableRequest)
        {
            var serviceResult = await _gateService.MakeGateAvailableAsync(makeGateAvailableRequest);

            await _hub.Clients.All.SendAsync("GateAvailable", new { gateId = serviceResult.GateId, planeId = serviceResult.PlaneId, gateStatus = serviceResult.GateStatus, planeStatus = serviceResult.PlaneStatus });

            return StatusCode((int)HttpStatusCode.OK, serviceResult);
        }

    }
}

