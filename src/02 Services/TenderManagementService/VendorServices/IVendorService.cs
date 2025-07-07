using TenderManagementDAL.Models;
using TenderManagementService.VendorServices.Models;

namespace TenderManagementService.VendorServices
{
    public interface IVendorService
    {
        (List<Vendor>?, long) GetPaginatedVendors(GetVendorsPaginatedServiceRequest request);
        Task<(List<Vendor>?, long)> GetPaginatedVendorsAsync(GetVendorsPaginatedServiceRequest request);
        (List<Vendor>?, long) GetAllVendors(GetVendorsPaginatedServiceRequest request);
        Task<(List<Vendor>?, long)> GetAllVendorsAsync(GetVendorsPaginatedServiceRequest request);
        Vendor? GetVendor(int id);
        Task<Vendor?> GetVendorAsync(int id);
        AddVendorServiceResponse AddVendor(AddVendorServiceRequest request);
        Task<AddVendorServiceResponse> AddVendorAsync(AddVendorServiceRequest request);
        EditVendorServiceResponse EditVendor(EditVendorServiceRequest request, string userId);
        DeleteVendorServiceResponse DeleteVendor(int id, string userId);
    }
}
