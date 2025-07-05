using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public interface IReadableTenderRepository : IReadRepository<Tender, int>
{
}