using Microsoft.EntityFrameworkCore;
using System;
using THY.GatePlanner.Infrastructure.Persistence.UOW;
using THY.GatePlanner.Model.Entities;
using THY.GatePlanner.Model.Enums;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;

namespace THY.GatePlanner.Service.GateService
{
    public class GateService : IGateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

      
        //[TODO] filter by code if code exists
        public async Task<List<GetGatesResponse>> GetGatesAsync(GetGatesRequest? request)
        {
            try
            {
                var response = new List<GetGatesResponse>();

                var gateRepository = _unitOfWork.GetRepository<Gate>();

                response = await gateRepository
                    .GetAll()
                    .Select(x => new GetGatesResponse() { Code = x.Code, Location = x.Location, Size = (SizeEnum)x.Size, Id = x.Id , GateStatus = x.GateStatus}).ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        public async Task<List<GetAvailableGatesBySizeResponse>> GetAvailableGatesBySizeAsync(GetAvailableGatesBySizeRequest request)
        {
            try
            {
                var response = new List<GetAvailableGatesBySizeResponse>();

                var gateRepository = _unitOfWork.GetRepository<Gate>();

                response = await gateRepository
                    .GetAll(x => x.GateStatus == GateStatus.Available.GetHashCode() && x.Size == request.Size)
                    .Select(x => new GetAvailableGatesBySizeResponse() { Code = x.Code, Location = x.Location, Id = x.Id })
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IsThereAvailableGateResponse> IsThereAvailableGateAsync()
        {
            try
            {
                var response = new IsThereAvailableGateResponse();

                var gateRepository = _unitOfWork.GetRepository<Gate>();

                response.IsThereAvailableGate = await gateRepository.GetAll(x => x.GateStatus == GateStatus.Available.GetHashCode()).AnyAsync();
                    
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}

