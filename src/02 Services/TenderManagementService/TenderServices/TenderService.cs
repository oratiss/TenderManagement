using Mapster;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.ReadRepositories;
using TenderManagementDAL.UnitOfWorks;
using TenderManagementService.AbstractModels;
using TenderManagementService.TenderServices.Models;

namespace TenderManagementService.TenderServices
{
    public class TenderService(IReadableTenderRepository tenderReadRepository, ITenderManagementUnitOfWork unitOfWork) : ITenderService
    {
        public (List<Tender>?, long) GetPaginatedTenders(GetTendersServiceRequest request)
        {
            var (paginatedTenders, count) = tenderReadRepository.GetPaged(request.PageSize!.Value, request.PageIndex!.Value, request.SortBy, request.SortOrder);
            return (paginatedTenders?.ToList(), count);
        }

        public async Task<(List<Tender>?, long)> GetPaginatedTendersAsync(GetTendersServiceRequest request)
        {
            var (paginatedTenders, count) = await tenderReadRepository.GetPagedAsync(request.PageSize!.Value, request.PageIndex!.Value, request.SortBy, request.SortOrder);
            return (paginatedTenders?.ToList(), count);
        }

        public (List<Tender>?, long) GetAllTenders(GetTendersServiceRequest request)
        {
            var (paginatedTenders, count) = tenderReadRepository.GetAll(request.SortBy, request.SortOrder);
            return (paginatedTenders?.ToList(), count);
        }

        public async Task<(List<Tender>?, long)> GetAllTendersAsync(GetTendersServiceRequest request)
        {
            var (paginatedTenders, count) = await tenderReadRepository.GetAllAsync(request.SortBy, request.SortOrder);
            return (paginatedTenders?.ToList(), count);
        }

        public Tender? GetTender(int id)
        {
            var existingTender = tenderReadRepository.GetById(id);
            return existingTender;
        }

        public async Task<Tender?> GetTenderAsync(int id)
        {
            var existingTender = await tenderReadRepository.GetByIdAsync(id);
            return existingTender;
        }

        public AddTenderServiceResponse AddTender(AddTenderServiceRequest request)
        {
            AddTenderServiceResponse response = new();
            var existingTender = tenderReadRepository.GetByTitle(request.Title);
            if (existingTender is not null)
            {
                response.Errors =
                [
                    new ServiceError()
                    {
                        Code = 400,
                        Message = "An existing tender found with same title."
                    }
                ];
                response.IsSuccessfull = false;
                return response;
            }

            Tender toBeAddedTender = request.Adapt<Tender>();
            toBeAddedTender.Id = 0;
            var addedTender = unitOfWork.WritableTenderRepository.Add(toBeAddedTender);
            unitOfWork.SaveChanges();
            response.Data = addedTender;
            response.IsSuccessfull = true;
            return response;
        }

        public async Task<AddTenderServiceResponse> AddTenderAsync(AddTenderServiceRequest request)
        {
            AddTenderServiceResponse response = new();
            var existingTender = tenderReadRepository.GetByTitle(request.Title);
            if (existingTender is not null)
            {
                response.Errors =
                [
                    new ServiceError()
                    {
                        Code = 400,
                        Message = "An existing tender found with same title."
                    }
                ];
                response.IsSuccessfull = false;
                return response;
            }

            Tender toBeAddedTender = request.Adapt<Tender>();
            toBeAddedTender.Id = 0;
            toBeAddedTender.ModifiedDate = DateTime.UtcNow;
            var addedTender = await unitOfWork.WritableTenderRepository.AddAsync(toBeAddedTender);
            await unitOfWork.SaveChangesAsync();
            response.Data = addedTender;
            response.IsSuccessfull = true;
            return response;
        }

        public EditTenderServiceResponse EditTender(EditTenderServiceRequest request, string userId)
        {
            EditTenderServiceResponse response = new();
            var existingTender = tenderReadRepository.GetById(request.Id);
            if (existingTender is null)
            {
                response.Errors =
                [
                    new ServiceError()
                    {
                        Code = 404,
                        Message = "No existing tender found to be edited."
                    }
                ];
                response.IsSuccessfull = false;
                return response;
            }

            existingTender = request.Adapt<Tender>();
            existingTender.ModifiedDate = DateTime.UtcNow;
            existingTender.ModifierUserId = userId;
            unitOfWork.WritableTenderRepository.Edit(existingTender);
            response.Data = existingTender;
            response.IsSuccessfull = true;
            return response;
        }

        public DeleteTenderServiceResponse DeleteTender(int id, string userId)
        {
            DeleteTenderServiceResponse response = new();
            var existingTender = tenderReadRepository.GetById(id);
            if (existingTender is null)
            {
                response.Errors =
                [
                    new ServiceError()
                    {
                        Code = 404,
                        Message = "No existing tender found to be deleted."
                    }
                ];
                response.IsSuccessfull = false;
                return response;
            }

            existingTender.ModifiedDate = DateTime.UtcNow;
            existingTender.ModifierUserId = userId;
            unitOfWork.WritableTenderRepository.Delete(existingTender);
            response.IsSuccessfull = true;
            return response;
        }

    }
}
