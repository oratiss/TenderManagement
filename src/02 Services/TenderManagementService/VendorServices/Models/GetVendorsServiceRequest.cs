using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.AbstractModels;
using TenderManagementService.BidServices.Models;

namespace TenderManagementService.VendorServices.Models;

public class GetVendorsServiceRequest : IServiceRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }

    public ICollection<GetBidServiceResponse>? Bids { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public SortOrder SortOrder { get; set; }
    public string SortBy { get; set; }
}