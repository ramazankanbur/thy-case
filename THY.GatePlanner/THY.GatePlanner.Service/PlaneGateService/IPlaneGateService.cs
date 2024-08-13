using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;

namespace THY.GatePlanner.Service.PlaneGateService
{
    public interface IPlaneGateService
    {
        Task<AssignGateToPlaneResponse> AssignGateToPlane(AssignGateToPlaneRequest request);
        Task<List<GetPlaneGatesResponse>> GetPlaneGatesAsync(GetPlaneGatesRequest? request);
        
    }
}
