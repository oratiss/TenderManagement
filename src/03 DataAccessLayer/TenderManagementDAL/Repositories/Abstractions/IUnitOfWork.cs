namespace TenderManagementDAL.Repositories.Abstractions;

public interface IUnitOfWork
{
    bool IsDisposed { get; }

    void SaveChanges();

    Task SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();
}