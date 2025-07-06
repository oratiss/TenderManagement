using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementDAL.Models;

namespace TenderManagementService.BidServices.Models
{
    public class GetBidServiceResponse
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime SubmissionDateTime { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; } = null!;
    }
}
