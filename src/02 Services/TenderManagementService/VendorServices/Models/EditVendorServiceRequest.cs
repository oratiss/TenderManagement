namespace TenderManagementService.VendorServices.Models;

public class EditVendorServiceRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
}