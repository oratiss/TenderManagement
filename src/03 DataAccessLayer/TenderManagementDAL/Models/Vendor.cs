using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models;

public class Vendor: IEntity , ISoftDeleteEnabled
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    
    public ICollection<Bid>? Bids { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifierUserId { get; set; }
}