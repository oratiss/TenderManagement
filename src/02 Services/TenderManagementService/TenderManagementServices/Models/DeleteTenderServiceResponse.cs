using TenderManagementDAL.Models;
using TenderManagementService.AbstractModels;

namespace TenderManagementService.TenderManagementServices.Models;

public class DeleteTenderServiceResponse :  IServiceResponse<Tender>
{
    public Tender? Data { get; set; }
    public List<ServiceError>? Errors { get; set; }
    public ServicePaginationMetaData? PaginationMetaData { get; set; }
    public bool IsSuccessfull { get; set; }
}