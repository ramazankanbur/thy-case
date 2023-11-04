using System;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;

namespace THY.GatePlanner.Service.GateService
{
    public class GateService : IGateService
    {
        Task<GetGatesResponse> IGateService.GetGatesAsync(GetGatesRequest? request)
        {
            throw new NotImplementedException();
        }
    }
}

