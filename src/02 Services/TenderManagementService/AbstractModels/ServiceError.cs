namespace TenderManagementService.AbstractModels;

public class ServiceError
{
    public string Message { get; set; } = null!;
    public int? Code { get; set; }
}