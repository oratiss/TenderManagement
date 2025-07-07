using TenderManagementDAL.Models;

namespace TenderManagementApi.DTOs.Responses;

public class BidResponse
{
    public long Id { get; set; }
    public int TenderId { get; set; }
    public int VendorId { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public DateTime SubmissionDateTime { get; set; }
    public string Status { get; set; } = StatusType.Pending.ToString();
}