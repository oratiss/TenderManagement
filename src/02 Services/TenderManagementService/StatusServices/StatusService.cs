using Mapster;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.ReadRepositories;
using TenderManagementService.StatusServices.Models;

namespace TenderManagementService.StatusServices;

public class StatusService (IReadableStatusRepository readableStatusRepository) : IStatusService
{
    public GetAllStatusesServiceResponse GetAllStatuses()
    {
        GetAllStatusesServiceResponse serviceResponse = new();

        var (allStatuses, count) = readableStatusRepository.GetAll(null, "Id");
        var statuses = allStatuses as Status[] ?? allStatuses!.ToArray();

        if (statuses!.Any())
        {
            serviceResponse.Data = null;
            serviceResponse.IsSuccessfull = true;
            return serviceResponse;
        }

        serviceResponse.Data = statuses!.ToList().Select(x => x.Adapt<StatusServiceResponse>()).ToList();
        serviceResponse.IsSuccessfull = true;
        serviceResponse.PaginationMetaData = new()
        {
            PageIndex = 1,
            PageSize = count,
            TotalCount = count,
            TotalPages = 1
        };
        return serviceResponse;
    }
}