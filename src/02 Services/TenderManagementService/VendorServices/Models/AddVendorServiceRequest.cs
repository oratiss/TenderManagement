namespace TenderManagementService.VendorServices.Models;

public class AddVendorServiceRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
}