namespace TenderManagementApi.DTOs.Abstractions;

public class PaginationMetaData
{
    public long TotalCount { get; set; }

    public long PageSize { get; set; }

    public long TotalPages { get; set; }

    public long PageIndex { get; set; }
}