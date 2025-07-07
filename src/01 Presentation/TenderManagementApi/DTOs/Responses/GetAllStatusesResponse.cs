namespace TenderManagementApi.DTOs.Responses;

public class GetAllStatusesResponse
{
    public List<GetStatusResponse>? Statuses { get; set; } = new();
}

public class GetStatusResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}