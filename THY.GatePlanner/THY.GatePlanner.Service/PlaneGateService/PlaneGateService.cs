using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THY.GatePlanner.Infrastructure.Persistence.UOW;
using THY.GatePlanner.Model.Entities;
using THY.GatePlanner.Model.Enums;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;
using THY.GatePlanner.Service.GateService;

namespace THY.GatePlanner.Service.PlaneGateService
{
    public class PlaneGateService : IPlaneGateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGateService _gateService;
        public PlaneGateService(IUnitOfWork unitOfWork, IGateService gateService)
        {
            _unitOfWork = unitOfWork;
            _gateService = gateService;
        }
        public async Task<AssignGateToPlaneResponse> AssignGateToPlane(AssignGateToPlaneRequest request)
        {
            try
            {
                var response = new AssignGateToPlaneResponse();

                var gateRepository = _unitOfWork.GetRepository<Gate>();
                var planeRepository = _unitOfWork.GetRepository<Plane>();
                var planeGateRepository = _unitOfWork.GetRepository<PlaneGate>();

                var planeToAssign = await planeRepository.GetByIdAsync(request.PlaneId);

                if(planeToAssign == default)
                    return response;

                var availableGatesToAssign = await _gateService.GetAvailableGatesBySizeAsync(new GetAvailableGatesBySizeRequest() { Size = planeToAssign!.Size });

                if (availableGatesToAssign.Any())
                {
                    var nearestGateCode = GetNearestGateCode(availableGatesToAssign.Select(x => (x.Code, x.Location)).ToList());

                    var nearestGate = gateRepository.GetAll(x => x.Code == nearestGateCode).FirstOrDefault();

                    planeGateRepository.Add(new PlaneGate()
                    {
                        GateId = nearestGate!.Id,
                        PlaneId = request.PlaneId,
                        PassengerOffboardingDuration =15,

                    });

                    planeToAssign.PlaneStatus = PlaneStatus.OnGate.GetHashCode();
                    planeRepository.Update(planeToAssign);

                    nearestGate.GateStatus = GateStatus.InUse.GetHashCode();
                    gateRepository.Update(nearestGate);



                    response.PlaneId = request.PlaneId;
                    response.GateId = nearestGate.Id;

                    await _unitOfWork.SaveChangesAsync();
                }

                return response;


            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<List<GetPlaneGatesResponse>> GetPlaneGatesAsync(GetPlaneGatesRequest? request)
        {
            try
            {
                var response = new List<GetPlaneGatesResponse>();

                var planeGateRepository = _unitOfWork.GetRepository<PlaneGate>();

                response = await planeGateRepository
                    .GetAll()
                    .Select(x => new GetPlaneGatesResponse() { GateCode = x.Gate.Code, GateId = x.GateId, GateLocation = x.Gate.Location, GateSize = x.Gate.Size, GateStatus = x.Gate.GateStatus , PassengerOffboardingDuration = x.PassengerOffboardingDuration})
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GetNearestGateCode(List<(string code, string location)> gateList)
        {
            var nearestCode = "";
            double nearestDistance = int.MaxValue;

            foreach (var gate in gateList)
            {
                double locationx = Convert.ToDouble(gate.location.Split(":")[0]);
                double locationy = Convert.ToDouble(gate.location.Split(":")[1]);

                var hypotenuse = Math.Sqrt((locationx * locationx) + (locationy * locationy));

                if (nearestDistance > hypotenuse)
                {
                    nearestDistance = hypotenuse;
                    nearestCode = gate.code;
                }
            }

            return nearestCode;
        }
    }
}
