
using TenderManagementService.BidServices.Models;

namespace TenderManagementService.BidServices
{
    public interface IBidService
    {
        GetBidServiceResponse AddBid(AddBidServiceRequest request);
        Task<GetBidServiceResponse> AddBidAsync(AddBidServiceRequest request);
        GetBidServiceResponse EditBid(EditBidServiceRequest request, string modifierUserId);
    }
}
