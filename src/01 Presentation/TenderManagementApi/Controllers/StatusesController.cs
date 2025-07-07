using System.Net;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenderManagementApi.Controllers.Abstractions;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementService.StatusServices;

namespace TenderManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusesController(IStatusService statusService) : ControllerBase
{
    /// <summary>
    /// Returns All Statuses
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "admin,vendor")] // Use the role that works for your 'admin' token
    public ActionResult<ApiResponse<GetAllStatusesResponse?>> GetAll()
    {
        ApiResponse<GetAllStatusesResponse> response = new();
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        var statusesServiceResponse = statusService.GetAllStatuses();

        response.Data = new()
        {
            Statuses = statusesServiceResponse.Data is null ? null : statusesServiceResponse.Data!.Select(x=>x.Adapt<GetStatusResponse>()).ToList()
        };

        response.PaginationMetaData = statusesServiceResponse.Data is null ? null : new PaginationMetaData
        {
            PageIndex = 1,
            PageSize = statusesServiceResponse.PaginationMetaData!.PageSize,
            TotalCount = statusesServiceResponse.PaginationMetaData!.TotalCount,
            TotalPages = 1
        };
        response.Succeeded = true;
        httpStatusCode = HttpStatusCode.OK;

        return StatusCode((int)httpStatusCode, response);
    }


}