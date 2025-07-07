using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementService.CategoryServices;

namespace TenderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoriesService) : ControllerBase
    {
        /// <summary>
        /// Returns All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,vendor")] // Use the role that works for your 'admin' token
        public ActionResult<ApiResponse<GetAllCategoriesResponse?>> GetAll()
        {
            ApiResponse<GetAllCategoriesResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            var categoriesServiceResponse = categoriesService.GetAllCategories();

            response.Data = new()
            {
                Categories = categoriesServiceResponse.Data is null ? null : categoriesServiceResponse.Data!.Select(x=>x.Adapt<GetCategoryResponse>()).ToList()
            };

            response.PaginationMetaData = categoriesServiceResponse.Data is null ? null : new PaginationMetaData
            {
                PageIndex = 1,
                PageSize = categoriesServiceResponse.PaginationMetaData!.PageSize,
                TotalCount = categoriesServiceResponse.PaginationMetaData!.TotalCount,
                TotalPages = 1
            };
            response.Succeeded = true;
            httpStatusCode = HttpStatusCode.OK;

            return StatusCode((int)httpStatusCode, response);
        }


    }
}
