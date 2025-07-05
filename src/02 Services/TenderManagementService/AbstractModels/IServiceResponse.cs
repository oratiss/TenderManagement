namespace TenderManagementService.AbstractModels;

public interface IServiceResponse<TEntity> where TEntity : class
{
    TEntity? Data { get; set; }
    List<ServiceError>? Errors { get; set; }
    ServicePaginationMetaData? PaginationMetaData { get; set; }
    public bool IsSuccessfull { get; set; }
}