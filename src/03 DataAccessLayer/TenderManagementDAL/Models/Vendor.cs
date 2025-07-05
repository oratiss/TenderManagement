using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models;

public class Vendor: IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    
    public ICollection<Bid>? Bids { get; set; }
}