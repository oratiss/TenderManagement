using TenderManagementDAL.Models;
using TenderManagementService.TenderServices.Models;

namespace TenderManagementService.TenderServices;

public interface ITenderService
{
    (List<Tender>?, long) GetPaginatedTenders(GetTendersPaginatedServiceRequest request);
    Task<(List<Tender>?, long)> GetPaginatedTendersAsync(GetTendersPaginatedServiceRequest request);
    (List<Tender>?, long) GetAllTenders(GetTendersPaginatedServiceRequest request);
    Task<(List<Tender>?, long)> GetAllTendersAsync(GetTendersPaginatedServiceRequest request);
    Tender? GetTender(int id);
    Task<Tender?> GetTenderAsync(int id);
    AddTenderServiceResponse AddTender(AddTenderServiceRequest request);
    Task<AddTenderServiceResponse> AddTenderAsync(AddTenderServiceRequest request);
    EditTenderServiceResponse EditTender(EditTenderServiceRequest request, string userId);
    DeleteTenderServiceResponse DeleteTender(int id, string userId);
}