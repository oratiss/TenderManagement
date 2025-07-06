using Mapster;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.ReadRepositories;
using TenderManagementDAL.UnitOfWorks;
using TenderManagementService.AbstractModels;
using TenderManagementService.VendorServices.Models;

namespace TenderManagementService.VendorServices;

public class VendorService(IReadableVendorRepository vendorReadRepository, ITenderManagementUnitOfWork unitOfWork) : IVendorService
{
    public (List<Vendor>?, long) GetPaginatedVendors(GetVendorsServiceRequest request)
    {
        var (paginatedVendors, count) = vendorReadRepository.GetPaged(request.PageSize!.Value, request.PageIndex!.Value, request.SortBy, request.SortOrder);
        return (paginatedVendors?.ToList(), count);
    }

    public async Task<(List<Vendor>?, long)> GetPaginatedVendorsAsync(GetVendorsServiceRequest request)
    {
        var (paginatedVendors, count) = await vendorReadRepository.GetPagedAsync(request.PageSize!.Value, request.PageIndex!.Value, request.SortBy, request.SortOrder);
        return (paginatedVendors?.ToList(), count);
    }

    public (List<Vendor>?, long) GetAllVendors(GetVendorsServiceRequest request)
    {
        var (paginatedVendors, count) = vendorReadRepository.GetAll(request.SortBy, request.SortOrder);
        return (paginatedVendors?.ToList(), count);
    }

    public async Task<(List<Vendor>?, long)> GetAllVendorsAsync(GetVendorsServiceRequest request)
    {
        var (paginatedVendors, count) = await vendorReadRepository.GetAllAsync(request.SortBy, request.SortOrder);
        return (paginatedVendors?.ToList(), count);
    }

    public Vendor? GetVendor(int id)
    {
        var existingTender = vendorReadRepository.GetById(id);
        return existingTender;
    }

    public Task<Vendor?> GetVendorAsync(int id)
    {
        var existingTender = vendorReadRepository.GetByIdAsync(id);
        return existingTender;
    }

    public AddVendorServiceResponse AddVendor(AddVendorServiceRequest request)
    {
        AddVendorServiceResponse response = new();
        var existingVendor = vendorReadRepository.GetByTitle(request.Title);
        if (existingVendor is not null)
        {
            response.Errors =
            [
                new ServiceError()
                {
                    Code = 400,
                    Message = "An existing vendor found with same title."
                }
            ];
            response.IsSuccessfull = false;
            return response;
        }

        var toBeAddedVendor = request.Adapt<Vendor>();
        toBeAddedVendor.Id = 0;
        var addedVendor = unitOfWork.WritableVendorRepository.Add(toBeAddedVendor);
        unitOfWork.SaveChanges();
        response.Data = addedVendor.Adapt<VendorServiceResponse>();
        response.IsSuccessfull = true;
        return response;
    }

    public async Task<AddVendorServiceResponse> AddVendorAsync(AddVendorServiceRequest request)
    {
        AddVendorServiceResponse response = new();
        var existingVendor = await vendorReadRepository.GetByTitleAsync(request.Title);
        if (existingVendor is not null)
        {
            response.Errors =
            [
                new ServiceError()
                {
                    Code = 400,
                    Message = "An existing vendor found with same title."
                }
            ];
            response.IsSuccessfull = false;
            return response;
        }

        var toBeAddedVendor = request.Adapt<Vendor>();
        toBeAddedVendor.Id = 0;
        var addedVendor = await unitOfWork.WritableVendorRepository.AddAsync(toBeAddedVendor);
        await unitOfWork.SaveChangesAsync();
        response.Data = addedVendor.Adapt<VendorServiceResponse>();
        response.IsSuccessfull = true;
        return response;
    }

    public EditVendorServiceResponse EditVendor(EditVendorServiceRequest request, string userId)
    {
        EditVendorServiceResponse response = new();
        var existingVendor = vendorReadRepository.GetById(request.Id);
        if (existingVendor is null)
        {
            response.Errors =
            [
                new ServiceError()
                {
                    Code = 404,
                    Message = "No existing vendor found to be edited."
                }
            ];
            response.IsSuccessfull = false;
            return response;
        }

        var existingVendorWithGivenTitle = vendorReadRepository.GetByTitle(request.Title);
        {
            if (existingVendorWithGivenTitle is not null && existingVendorWithGivenTitle.Id != request.Id)
            {
                response.Errors =
                [
                    new ServiceError()
                    {
                        Code = 400,
                        Message = "Another existing vendor found with given Title. for keeping unique title, you should change your request title."
                    }
                ];
                response.IsSuccessfull = false;
                return response;
            }
        }

        existingVendor = request.Adapt<Vendor>();
        existingVendor.ModifiedDate = DateTime.UtcNow;
        existingVendor.ModifierUserId = userId;
        unitOfWork.WritableVendorRepository.Edit(existingVendor);
        response.Data = existingVendor.Adapt<VendorServiceResponse>();
        response.IsSuccessfull = true;
        return response;
    }

    public DeleteVendorServiceResponse DeleteVendor(int id, string userId)
    {
        DeleteVendorServiceResponse response = new();
        var existingVendor = vendorReadRepository.GetById(id);
        if (existingVendor is null)
        {
            response.Errors =
            [
                new ServiceError()
                {
                    Code = 404,
                    Message = "No existing vendor found to be deleted."
                }
            ];
            response.IsSuccessfull = false;
            return response;
        }

        existingVendor.ModifiedDate = DateTime.UtcNow;
        existingVendor.ModifierUserId = userId;
        unitOfWork.WritableVendorRepository.Delete(existingVendor);
        response.IsSuccessfull = true;
        return response;
    }
}