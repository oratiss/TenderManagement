using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models;

public class Bid : IEntity<long>, ISoftDeleteEnabled<long>
{

    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDateTime { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;
    public string? Comment { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifierUserId { get; set; }
}

