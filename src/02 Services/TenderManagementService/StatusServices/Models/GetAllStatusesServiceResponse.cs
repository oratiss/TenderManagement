using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementService.AbstractModels;

namespace TenderManagementService.StatusServices.Models
{
    public class GetAllStatusesServiceResponse : IServiceResponse<List<StatusServiceResponse>>
    {
        public List<StatusServiceResponse>? Data { get; set; }
        public List<ServiceError>? Errors { get; set; }
        public ServicePaginationMetaData? PaginationMetaData { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
