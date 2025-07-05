using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace TenderManagementDAL.Repositories.Abstractions;

public interface IEfDataContext : IDataContext
{
    DbSet<T> Set<T>() where T : class;
    EntityEntry Entry(object entity);
}