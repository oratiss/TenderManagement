namespace TenderManagementService.BidServices.Models;

public class BidServiceResponse
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDateTime { get; set; }
    public int StatusId { get; set; }
    public string Status { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public int VendorId { get; set; }
    public int TenderId { get; set; }
}