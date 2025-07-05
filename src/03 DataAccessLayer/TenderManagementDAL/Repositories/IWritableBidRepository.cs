using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories;

public interface IWritableBidRepository : IWriteRepository<Bid, long>
{
}