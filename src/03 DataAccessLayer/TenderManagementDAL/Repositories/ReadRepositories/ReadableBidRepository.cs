using System.Data;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableBidRepository(IDbConnection connection, string tableName)
    : ReadRepository<Bid, long>(connection, tableName), IReadableBidRepository
{
    
}