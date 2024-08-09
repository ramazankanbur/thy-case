using Microsoft.EntityFrameworkCore;
using THY.GatePlanner.Infrastructure.Persistence.UOW;
using THY.GatePlanner.Model.Entities;
using THY.GatePlanner.Model.Requests;
using THY.GatePlanner.Model.Responses;

namespace THY.GatePlanner.Service.PlaneService
{
    public class PlaneService : IPlaneService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<List<GetPlanesResponse>> GetPlanesAsync(GetPlanesRequest? request)
        {
            try
            {
                var response = new List<GetPlanesResponse>();

                var planeRepository = _unitOfWork.GetRepository<Plane>();

                response = await planeRepository
                    .GetAll()
                    .Select(x => new GetPlanesResponse() { Id = x.Id, PlaneStatus = x.PlaneStatus.GetHashCode(), Code = x.Code })
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<CreatePlaneResponse> CreatePlaneAsync(CreatePlaneRequest? request)
        {
            try
            {
                var response = new CreatePlaneResponse();

                var planeRepository = _unitOfWork.GetRepository<Plane>();

                var planeToAdd = new Plane()
                {
                    Code = request.Code,
                    Size = request.Size

                };

                planeRepository.Add(planeToAdd);

                await _unitOfWork.SaveChangesAsync();

                response.Id = planeToAdd.Id;
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
