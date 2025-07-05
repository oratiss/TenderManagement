using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Contexts
{
    public class TenderManagementDbContext(DbContextOptions<TenderManagementDbContext> options) : IdentityDbContext<User>(options), IEfDataContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = StatusType.Open.Value },
                new Status { Id = 2, Name = StatusType.Closed.Value },
                new Status { Id = 3, Name = StatusType.Pending.Value },
                new Status { Id = 4, Name = StatusType.Approved.Value },
                new Status { Id = 5, Name = StatusType.Rejected.Value }
            );
        }
    }

}
