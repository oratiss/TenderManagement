using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories;

public interface IWritableVendorRepository : IWriteRepository<Vendor, int>
{
}