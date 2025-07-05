using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories;

public class WritableCategoryRepository(IEfDataContext context)
    : WriteRepository<Category, int, TenderManagementDbContext>(context), IWritableCategoryRepository;