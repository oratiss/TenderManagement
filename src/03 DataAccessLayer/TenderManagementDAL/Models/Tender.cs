using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models
{
    public class Tender: IEntity, ISoftDeleteEnabled
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
        public bool IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifierUserId { get; set; } //todo: add 36 char length in context
    }




}
