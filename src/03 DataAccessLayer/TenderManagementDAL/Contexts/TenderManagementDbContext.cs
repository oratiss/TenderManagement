using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Contexts
{
    public class TenderManagementDbContext(DbContextOptions<TenderManagementDbContext> options) : IdentityDbContext<User>(options), IEfDataContext
    {
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Status>().HasData(
                new Status { Id = 1, Name = StatusType.Open.Value },
                new Status { Id = 2, Name = StatusType.Closed.Value },
                new Status { Id = 3, Name = StatusType.Pending.Value },
                new Status { Id = 4, Name = StatusType.Approved.Value },
                new Status { Id = 5, Name = StatusType.Rejected.Value }
            );
            builder.Entity<Status>().Property<string>("Name").HasMaxLength(20);

            builder.Entity<Tender>().HasQueryFilter(x => x.IsDeleted == false);
            builder.Entity<Tender>().Property<string>("Title").HasMaxLength(70);
            builder.Entity<Tender>().Property<string>("Description").HasMaxLength(1000);
            builder.Entity<Tender>().Property<string>("ModifierUserId").HasMaxLength(36); //string Guid
            builder.Entity<Tender>().HasMany<Bid>().WithOne(x => x.Tender).HasForeignKey(x => x.TenderId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Category>().HasQueryFilter(x => x.IsDeleted == false);
            builder.Entity<Category>().Property<string>("Name").HasMaxLength(100);
            builder.Entity<Category>().Property<string>("ModifierUserId").HasMaxLength(36);

            builder.Entity<Vendor>().HasQueryFilter(x => x.IsDeleted == false);
            builder.Entity<Vendor>().Property<string>("Title").HasMaxLength(100);
            builder.Entity<Vendor>().Property<string>("Description").HasMaxLength(500);
            builder.Entity<Vendor>().Property<string>("ModifierUserId").HasMaxLength(36);
            builder.Entity<Vendor>().HasMany<Bid>().WithOne(x => x.Vendor).HasForeignKey(x=>x.VendorId).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Bid>().HasQueryFilter(x => x.IsDeleted == false);
            builder.Entity<Bid>().Property<string>("Comment").HasMaxLength(1000);
            builder.Entity<Bid>().Property<string>("ModifierUserId").HasMaxLength(36); //string Guid




        }
    }

}
