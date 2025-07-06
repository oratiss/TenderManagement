using TenderManagementDAL.Models;

namespace TenderManagementApi.DTOs.Responses
{
    public class GetAllTendersResponse
    {
        public List<GetTenderResponse>? Tenders { get; set; } = new List<GetTenderResponse>();
    }

    public class GetTenderResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public int StatusId { get; set; }
        public string Status { get; set; } = null!;
    }

}
