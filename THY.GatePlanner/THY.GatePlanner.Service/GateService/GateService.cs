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

        async Task<List<GetGatesResponse>> IGateService.GetGatesAsync(GetGatesRequest? request)
        {
            try
            {
                var response = new List<GetGatesResponse>();

                var gateRepository = _unitOfWork.GetRepository<Gate>();

                response = await gateRepository
                    .GetAll()
                    .Select(x => new GetGatesResponse() { code = x.Code, location = x.Location, size = (SizeEnum)x.Size, id = x.Id.ToString() })
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
    }
}

