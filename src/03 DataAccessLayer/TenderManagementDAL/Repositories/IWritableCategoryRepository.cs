using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories;

public interface IWritableCategoryRepository : IWriteRepository<Category, int>
{
}
