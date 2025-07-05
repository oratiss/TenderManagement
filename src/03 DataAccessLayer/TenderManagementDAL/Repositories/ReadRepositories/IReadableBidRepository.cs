using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories
{
    public interface IReadableBidRepository : IReadRepository<Bid, long>
    {
    }
}
