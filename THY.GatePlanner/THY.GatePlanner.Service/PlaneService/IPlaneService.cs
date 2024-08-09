using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;

namespace THY.GatePlanner.Service.PlaneService
{
    public interface IPlaneService
    {
        Task<List<GetPlanesResponse>> GetPlanesAsync(GetPlanesRequest? request);
        Task<CreatePlaneResponse> CreatePlaneAsync(CreatePlaneRequest? request);

    }
}
