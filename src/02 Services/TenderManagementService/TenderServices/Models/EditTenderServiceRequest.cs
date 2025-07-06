using TenderManagementDAL.Models;

namespace TenderManagementService.TenderServices.Models
{
    public class EditTenderServiceRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;


        public ICollection<Bid>? Bids { get; set; }
    }
}
