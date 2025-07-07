using TenderManagementService.AbstractModels;

namespace TenderManagementService.CategoryServices.Models;

public class GetAllCategoryServiceResponse : IServiceResponse<List<CategoryServiceResponse>>
{
    public List<CategoryServiceResponse>? Data { get; set; }
    public List<ServiceError>? Errors { get; set; }
    public ServicePaginationMetaData? PaginationMetaData { get; set; }
    public bool IsSuccessfull { get; set; }
}