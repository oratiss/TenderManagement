using Mapster;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.ReadRepositories;
using TenderManagementService.CategoryServices.Models;

namespace TenderManagementService.CategoryServices;

public class CategoryService(IReadableCategoryRepository readableCategoryRepository) : ICategoryService
{
    public GetAllCategoryServiceResponse GetAllCategories()
    {
        GetAllCategoryServiceResponse serviceResponse = new();

        var (allCategories, count) = readableCategoryRepository.GetAll(null, "Id");

        var categories = allCategories as Category[] ?? allCategories!.ToArray();
        if (categories!.Any())
        {
            serviceResponse.Data = null;
            serviceResponse.IsSuccessfull = true;
            return serviceResponse;
        }

        serviceResponse.Data = categories!.ToList().Select(x => x.Adapt<CategoryServiceResponse>()).ToList();
        serviceResponse.IsSuccessfull = true;
        serviceResponse.PaginationMetaData = new()
        {
            PageIndex = 1,
            PageSize = count,
            TotalCount = count,
            TotalPages = 1
        };
        return serviceResponse;
    }
}