using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.WriteRepositories;

public interface IWritableBidRepository : IWriteRepository<Bid, long>
{
}