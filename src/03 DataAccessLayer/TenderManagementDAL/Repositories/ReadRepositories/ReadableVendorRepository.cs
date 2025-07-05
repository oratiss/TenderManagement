using System.Data;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableVendorRepository(IDbConnection connection, string tableName)
    : ReadRepository<Vendor, int>(connection, tableName), IReadableVendorRepository
{
}