using Azure.Core;
using Mapster;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.ReadRepositories;
using TenderManagementDAL.UnitOfWorks;
using TenderManagementService.AbstractModels;
using TenderManagementService.BidServices.Models;
using TenderManagementService.VendorServices.Models;

namespace TenderManagementService.BidServices;

public class BidService(IReadableBidRepository readBidRepoistory, ITenderManagementUnitOfWork unitOfWork) : IBidService
{
    public GetBidServiceResponse AddBid(AddBidServiceRequest request)
    {
        GetBidServiceResponse response = new();

        var toBeAddedBid = request.Adapt<Bid>();
        toBeAddedBid.Id = 0; //I have doubt here that is it required or not. on debug I will check
        var addedBid = unitOfWork.WritableBidRepository.Add(toBeAddedBid);
        unitOfWork.SaveChanges();
        response.Data = addedBid.Adapt<BidServiceResponse>();
        response.IsSuccessfull = true;
        return response;
    }

    public async Task<GetBidServiceResponse> AddBidAsync(AddBidServiceRequest request)
    {
        GetBidServiceResponse response = new();

        var toBeAddedBid = request.Adapt<Bid>();
        toBeAddedBid.Id = 0; //I have doubt here that is it required or not. on debug I will check
        var addedBid = await unitOfWork.WritableBidRepository.AddAsync(toBeAddedBid);
        await unitOfWork.SaveChangesAsync();
        response.Data = addedBid.Adapt<BidServiceResponse>();
        response.IsSuccessfull = true;
        return response;
    }

    public GetBidServiceResponse EditBid(EditBidServiceRequest request, string modifierUserId)
    {
        GetBidServiceResponse response = new();
        var existingBid = readBidRepoistory.GetById(request.Id);
        if (existingBid is null)
        {
            response.Errors =
            [
                new ServiceError()
                {
                    Code = 404,
                    Message = "No existing bid found to be edited."
                }
            ];
            response.IsSuccessfull = false;
            return response;
        }

        existingBid = request.Adapt<Bid>();
        existingBid.ModifiedDate = DateTime.UtcNow;
        existingBid.ModifierUserId = modifierUserId;
        unitOfWork.WritableBidRepository.Edit(existingBid);
        unitOfWork.SaveChanges();
        response.Data = existingBid.Adapt<BidServiceResponse>();
        response.IsSuccessfull = true;
        return response;
    }
}