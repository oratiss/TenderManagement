using TenderManagementDAL.Models;
using TenderManagementService.TenderServices.Models;

namespace TenderManagementService.TenderServices;

public interface ITenderService
{
    (List<Tender>?, long) GetPaginatedTenders(GetTendersServiceRequest request);
    Task<(List<Tender>?, long)> GetPaginatedTendersAsync(GetTendersServiceRequest request);
    (List<Tender>?, long) GetAllTenders(GetTendersServiceRequest request);
    Task<(List<Tender>?, long)> GetAllTendersAsync(GetTendersServiceRequest request);
    Tender? GetTender(int id);
    Task<Tender?> GetTenderAsync(int id);
    EditTenderServiceResponse EditTender(EditTenderServiceRequest request);
    DeleteTenderServiceResponse DeleteTender(int id);
}