using Microsoft.EntityFrameworkCore.Query;
using TenderManagementDAL.Models.Abstarctions;

namespace TenderManagementApi.DTOs.Responses
{
    public class AddTenderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
