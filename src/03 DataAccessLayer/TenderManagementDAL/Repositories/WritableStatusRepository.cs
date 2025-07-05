using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories;

public class WritableStatusRepository(IEfDataContext context)
    : WriteRepository<Status, long, TenderManagementDbContext>(context), IWritableStatusRepository
{

}