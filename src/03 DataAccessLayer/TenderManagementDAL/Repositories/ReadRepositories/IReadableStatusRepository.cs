using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public interface IReadableStatusRepository : IReadRepository<Status, long>
{
}