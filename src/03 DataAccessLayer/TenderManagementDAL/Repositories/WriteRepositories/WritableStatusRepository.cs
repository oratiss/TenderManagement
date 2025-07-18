﻿using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.WriteRepositories;

public class WritableStatusRepository(IEfDataContext context)
    : WriteRepository<Status, long, TenderManagementDbContext>(context), IWritableStatusRepository
{

}