using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementService.AbstractModels
{
    public interface IPaginatedServiceRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public SortOrder SortOrder { get; set; }
        public string SortBy { get; set; }
    }
}
