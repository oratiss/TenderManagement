using TenderManagementDAL.Models;

namespace TenderManagementApi.DTOs.Responses;

public class GetAllVendorsResponse
{
    public List<GetVendorResponse>? Tenders { get; set; } = new List<GetVendorResponse>();
}

public class GetVendorResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public ICollection<Bid>? Bids { get; set; }
}