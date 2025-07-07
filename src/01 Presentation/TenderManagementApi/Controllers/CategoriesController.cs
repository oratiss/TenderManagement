using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TenderManagementApi.Controllers.Abstractions;
using TenderManagementApi.DTOs;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.CategoryServices;
using TenderManagementService.TenderServices;
using TenderManagementService.TenderServices.Models;

namespace TenderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoriesService, IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
    {
        /// <summary>
        /// Returns All Categories for admin
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
