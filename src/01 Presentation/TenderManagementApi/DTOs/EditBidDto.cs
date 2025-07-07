using TenderManagementDAL.Models;

namespace TenderManagementApi.DTOs;

public class EditBidDto
{
    public int TenderId { get; set; }
    public int VendorId { get; set; }
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public int StatusId { get; set; }
}