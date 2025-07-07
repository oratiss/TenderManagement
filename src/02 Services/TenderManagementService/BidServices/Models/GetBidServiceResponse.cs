using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementDAL.Models;
using TenderManagementService.AbstractModels;

namespace TenderManagementService.BidServices.Models
{
    public class GetBidServiceResponse : IServiceResponse<BidServiceResponse>
    {
        public BidServiceResponse? Data { get; set; }
        public List<ServiceError>? Errors { get; set; }
        public ServicePaginationMetaData? PaginationMetaData { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
