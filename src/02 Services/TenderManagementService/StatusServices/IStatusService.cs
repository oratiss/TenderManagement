using TenderManagementService.StatusServices.Models;

namespace TenderManagementService.StatusServices
{
    public interface IStatusService
    {
        public GetAllStatusesServiceResponse GetAllStatuses();
    }
}
