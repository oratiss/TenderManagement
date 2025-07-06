using TenderManagementDAL.Models;
using TenderManagementService.AbstractModels;

namespace TenderManagementService.VendorServices.Models;

public class DeleteVendorServiceResponse : IServiceResponse<VendorServiceResponse>
{
    public VendorServiceResponse? Data { get; set; }
    public List<ServiceError>? Errors { get; set; }
    public ServicePaginationMetaData? PaginationMetaData { get; set; }
    public bool IsSuccessfull { get; set; }
}