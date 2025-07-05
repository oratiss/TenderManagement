using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models
{
    public class Status: IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Tender>? Tenders { get; set; }

        public ICollection<Bid>? Bids { get; set; }
    }


}
