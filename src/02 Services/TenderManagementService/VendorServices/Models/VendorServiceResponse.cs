using TenderManagementService.BidServices.Models;

namespace TenderManagementService.VendorServices.Models;

public class VendorServiceResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }

    public ICollection<GetBidServiceResponse>? Bids { get; set; }
}