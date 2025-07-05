using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories;

public interface IWritableTenderRepository : IWriteRepository<Tender, int>
{
}