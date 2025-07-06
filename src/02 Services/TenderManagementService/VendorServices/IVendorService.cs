using TenderManagementDAL.Models;
using TenderManagementService.VendorServices.Models;

namespace TenderManagementService.VendorServices
{
    public interface IVendorService
    {
        (List<Vendor>?, long) GetPaginatedVendors(GetVendorsServiceRequest request);
        Task<(List<Vendor>?, long)> GetPaginatedVendorsAsync(GetVendorsServiceRequest request);
        (List<Vendor>?, long) GetAllVendors(GetVendorsServiceRequest request);
        Task<(List<Vendor>?, long)> GetAllVendorsAsync(GetVendorsServiceRequest request);
        Vendor? GetVendor(int id);
        Task<Vendor?> GetVendorAsync(int id);
        AddVendorServiceResponse AddVendor(AddVendorServiceRequest request);
        Task<AddVendorServiceResponse> AddVendorAsync(AddVendorServiceRequest request);
        EditVendorServiceResponse EditVendor(EditVendorServiceRequest request, string userId);
        DeleteVendorServiceResponse DeleteVendor(int id, string userId);
    }
}
