using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.WriteRepositories;

public class WritableVendorRepository(IEfDataContext context)
    : WriteRepository<Vendor, int, TenderManagementDbContext>(context), IWritableVendorRepository
{

}