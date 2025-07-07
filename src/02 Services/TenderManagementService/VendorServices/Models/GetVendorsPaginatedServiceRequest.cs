using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.AbstractModels;
using TenderManagementService.BidServices.Models;

namespace TenderManagementService.VendorServices.Models;

public class GetVendorsPaginatedServiceRequest : IPaginatedServiceRequest
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public SortOrder SortOrder { get; set; }
    public string SortBy { get; set; } = "Id";
}