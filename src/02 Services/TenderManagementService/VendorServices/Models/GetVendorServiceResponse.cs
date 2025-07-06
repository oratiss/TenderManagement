using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementService.AbstractModels;

namespace TenderManagementService.VendorServices.Models
{
    public class GetVendorServiceResponse : IServiceResponse<VendorServiceResponse>
    {
        public VendorServiceResponse? Data { get; set; }
        public List<ServiceError>? Errors { get; set; }
        public ServicePaginationMetaData? PaginationMetaData { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
