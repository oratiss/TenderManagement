using TenderManagementDAL.Contexts;
using TenderManagementDAL.Repositories.WriteRepositories;

namespace TenderManagementDAL.UnitOfWorks
{
    public class TenderManagementUnitOfWork(TenderManagementDbContext dataContext): ITenderManagementUnitOfWork
    {
        private bool _disposed = false;

        public bool IsDisposed => _disposed;

        private IWritableBidRepository? _writableBidRepository;
        public IWritableBidRepository WritableBidRepository => _writableBidRepository ??= new WritableBidRepository(dataContext);


        private IWritableCategoryRepository? _writableCategoryRepository;
        public IWritableCategoryRepository WritableCategoryRepository => _writableCategoryRepository ??= new WritableCategoryRepository(dataContext);


        private IWritableTenderRepository? _writableTenderRepository;
        public IWritableTenderRepository WritableTenderRepository => _writableTenderRepository ??= new WritableTenderRepository(dataContext);


        private IWritableVendorRepository? _writableVendorRepository;
        public IWritableVendorRepository WritableVendorRepository => _writableVendorRepository ??= new WritableVendorRepository(dataContext);


        private IWritableStatusRepository? _writableStatusRepository;
        public IWritableStatusRepository WritableStatusRepository => _writableStatusRepository ??= new WritableStatusRepository(dataContext);
        

        public async Task BeginTransactionAsync()
        {
            await dataContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await dataContext.Database.CommitTransactionAsync();
        }

        public void SaveChanges()
        {
            dataContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
