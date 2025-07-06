using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public interface IReadableVendorRepository : IReadRepository<Vendor, int>
{
    Vendor? GetByTitle(string requestTitle);
    Task<Vendor?> GetByTitleAsync(string title);
}