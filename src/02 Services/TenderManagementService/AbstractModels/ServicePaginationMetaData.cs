namespace TenderManagementService.AbstractModels;

public class ServicePaginationMetaData
{
    public long TotalCount { get; set; }

    public long PageSize { get; set; }

    public long TotalPages { get; set; }

    public long PageIndex { get; set; }
}