using TenderManagementDAL.Models;

namespace TenderManagementApi.DTOs.Responses
{
    public class AddTenderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
