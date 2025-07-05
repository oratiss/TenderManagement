using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.WriteRepositories;

public interface IWritableTenderRepository : IWriteRepository<Tender, int>
{
}