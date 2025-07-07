using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenderManagementService.CategoryServices.Models;

namespace TenderManagementService.CategoryServices
{
    public interface ICategoryService
    {
        public GetAllCategoryServiceResponse GetAllCategories();
    }
}
