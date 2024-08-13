using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;
using THY.GatePlanner.Service.GateService;

namespace THY.GatePlanner.API.Controllers
{
    public class GateController : ControllerBase
    {
        private readonly IGateService _gateService;

        public GateController(IGateService gateService)
        {
            _gateService = gateService;
        }

        [HttpGet("Gate")]
        public async Task<ActionResult<GetGatesResponse>> GetGates([FromQuery] GetGatesRequest getGatesRequest)
        {
            var serviceResult = await _gateService.GetGatesAsync(getGatesRequest);

            var response = serviceResult;            

            return StatusCode((int)HttpStatusCode.OK, response);
        }

  
    }
}

