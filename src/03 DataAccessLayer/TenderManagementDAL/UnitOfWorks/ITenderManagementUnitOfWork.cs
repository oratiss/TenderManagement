using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementDAL.Repositories.WriteRepositories;

namespace TenderManagementDAL.UnitOfWorks
{
    public interface ITenderManagementUnitOfWork : IUnitOfWork
    {
        public IWritableBidRepository WritableBidRepository { get; }
        public IWritableTenderRepository WritableTenderRepository { get; }
        public IWritableVendorRepository WritableVendorRepository { get; }
        public IWritableCategoryRepository WritableCategoryRepository { get; }
        public IWritableStatusRepository WritableStatusRepository { get; }
    }
}
