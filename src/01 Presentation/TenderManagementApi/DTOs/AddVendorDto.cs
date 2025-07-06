using TenderManagementDAL.Models;

namespace TenderManagementApi.DTOs
{
    public class AddVendorDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
