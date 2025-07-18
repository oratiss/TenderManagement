﻿using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.WriteRepositories
{
    public class WritableTenderRepository(IEfDataContext context)
        : WriteRepository<Tender, int, TenderManagementDbContext>(context), IWritableTenderRepository
    {

    }
}
