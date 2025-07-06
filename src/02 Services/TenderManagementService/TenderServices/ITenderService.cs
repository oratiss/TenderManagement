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
    AddTenderServiceResponse AddTender(AddTenderServiceRequest request);
    Task<AddTenderServiceResponse> AddTenderAsync(AddTenderServiceRequest request);
    EditTenderServiceResponse EditTender(EditTenderServiceRequest request, string userId);
    DeleteTenderServiceResponse DeleteTender(int id, string userId);
}