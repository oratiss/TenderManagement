using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.WriteRepositories;

public class WritableBidRepository(IEfDataContext context)
    : WriteRepository<Bid, long, TenderManagementDbContext>(context), IWritableBidRepository
{

}