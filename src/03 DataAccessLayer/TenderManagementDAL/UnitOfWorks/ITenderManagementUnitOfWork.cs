using TenderManagementDAL.Repositories;

namespace TenderManagementDAL.UnitOfWorks
{
    public interface ITenderManagementUnitOfWork
    {
        public IWritableBidRepository WritableBidRepository { get; }
        public IWritableTenderRepository WritableTenderRepository { get; }
        public IWritableVendorRepository WritableVendorRepository { get; }
        public IWritableCategoryRepository WritableCategoryRepository { get; }
        public IWritableStatusRepository WritableStatusRepository { get; }
    }
}
