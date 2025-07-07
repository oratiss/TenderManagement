using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.AbstractModels;

namespace TenderManagementService.BidServices.Models
{
    public class AddBidServiceRequest
    {
        public int TenderId { get; set; }
        public int VendorId { get; set; }
        public decimal Amount { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; } = StatusType.Pending.ToString();
        
    }
}
