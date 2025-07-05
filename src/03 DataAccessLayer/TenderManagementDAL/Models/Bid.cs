using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models;

public class Bid: IEntity<long>
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDateTime { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

}

