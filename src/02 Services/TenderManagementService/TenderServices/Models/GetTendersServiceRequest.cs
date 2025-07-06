using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.AbstractModels;

namespace TenderManagementService.TenderServices.Models
{
    public class GetTendersServiceRequest : IServiceRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public SortOrder SortOrder { get; set; }
        public required string SortBy { get; set; } = "Id";
    }
}
