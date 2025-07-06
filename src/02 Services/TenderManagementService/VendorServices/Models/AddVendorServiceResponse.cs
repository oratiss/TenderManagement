using TenderManagementDAL.Models;
using TenderManagementService.AbstractModels;
using TenderManagementService.BidServices.Models;

namespace TenderManagementService.VendorServices.Models;

public class AddVendorServiceResponse : IServiceResponse<VendorServiceResponse>
{
    public VendorServiceResponse? Data { get; set; }
    public List<ServiceError>? Errors { get; set; }
    public ServicePaginationMetaData? PaginationMetaData { get; set; }
    public bool IsSuccessfull { get; set; }
}