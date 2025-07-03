using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementDAL.Models;

namespace TenderManagementDAL.Contexts
{
    public class TenderManagementDbContext(DbContextOptions<TenderManagementDbContext> options) : IdentityDbContext<User>(options)
    {

    }
}
