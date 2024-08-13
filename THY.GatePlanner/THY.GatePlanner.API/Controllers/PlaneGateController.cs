using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;
using THY.GatePlanner.Service.PlaneGateService;

namespace THY.GatePlanner.API.Controllers
{
 
    public class PlaneGateController : ControllerBase
    {
        private readonly IPlaneGateService _planeGateService;
        public PlaneGateController(IPlaneGateService planeGateService)
        {
            _planeGateService = planeGateService; 
        }

        [HttpGet("PlaneGate")]
        public async Task<ActionResult<GetPlaneGatesResponse>> GetGatePlanes([FromQuery] GetPlaneGatesRequest getGatesRequest)
        {
            try
            {
                var serviceResult = await _planeGateService.GetPlaneGatesAsync(getGatesRequest);

                var response = serviceResult;

                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

    }
}
