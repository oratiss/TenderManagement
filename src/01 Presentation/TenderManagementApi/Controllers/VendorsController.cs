using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TenderManagementApi.Controllers.Abstractions;
using TenderManagementApi.DTOs;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.VendorServices;
using TenderManagementService.VendorServices.Models;

namespace TenderManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsController(IVendorService vendorService, IHttpContextAccessor httpContextAccessor) 
    : BaseController(httpContextAccessor)
{

    // GET: api/<TendersController>
    [HttpGet("Get")]
    [Authorize(Roles = "admin,vendor")]
    public ActionResult<ApiResponse<GetAllVendorsResponse?>> GetAll()
    {
        ApiResponse<GetAllVendorsResponse> response = new();
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        var (vendors, count) = vendorService.GetAllVendors(new GetVendorsServiceRequest()
        {
            SortBy = "Id",
            SortOrder = SortOrder.Asc
        });

        response.Data = new()
        {
            Tenders = vendors is null ? null : vendors!.Adapt<List<GetVendorResponse>>()
        };

        response.PaginationMetaData = vendors is null ? null : new PaginationMetaData
        {
            PageIndex = 1,
            PageSize = count,
            TotalCount = count,
            TotalPages = 1
        };
        response.Succeeded = true;
        httpStatusCode = HttpStatusCode.OK;

        return StatusCode((int)httpStatusCode, response);
    }

    // GET api/<TendersController>/5
    [HttpGet("{id}")]
    [Authorize(Roles = "admin,vendor")]
    public ActionResult<ApiResponse<GetVendorResponse?>> Get([FromRoute] int id)
    {
        ApiResponse<GetVendorResponse> response = new();
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        var existingVendor = vendorService.GetVendor(id);
        if (existingVendor is null)
        {
            response.Errors.Add(new Error()
            {
                Code = 404,
                ErrorMessage = "No vendor Found With Given Id"
            });
            httpStatusCode = HttpStatusCode.NotFound;
            return StatusCode((int)httpStatusCode, response);
        }

        response.Data = new()
        {
            Id = existingVendor.Id,
            Title = existingVendor.Title,
            Description = existingVendor.Description,
            Bids = existingVendor.Bids
        };

        response.PaginationMetaData = new PaginationMetaData()
        {
            PageIndex = 1,
            PageSize = 1,
            TotalCount = 1,
            TotalPages = 1
        };
        response.Succeeded = true;
        httpStatusCode = HttpStatusCode.OK;

        return StatusCode((int)httpStatusCode, response);
    }

    // POST api/<TendersController>
    [HttpPost]
    [Authorize(Roles = "admin,vendor")]
    public ActionResult<ApiResponse<GetVendorResponse>> Post([FromBody] AddVendorDto addVendorRequest)
    {
        ApiResponse<GetVendorResponse> response = new();
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        //todo: validate request
            
        var serviceResponse = vendorService.AddVendor(addVendorRequest.Adapt<AddVendorServiceRequest>());
        if (serviceResponse.Errors!.Any())
        {
            switch (serviceResponse.Errors!.First().Code)
            {
                case 400:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
                case 500:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            response.Errors = new List<Error>();
            serviceResponse.Errors!.ForEach(e => response.Errors.Add(e.Adapt<Error>()));
            return StatusCode((int)httpStatusCode, response);
        }

        response.Data = serviceResponse.Data.Adapt<GetVendorResponse>();
        response.PaginationMetaData = new PaginationMetaData()
        {
            PageIndex = 1,
            PageSize = 1,
            TotalCount = 1,
            TotalPages = 1
        };
        response.Succeeded = true;
        httpStatusCode = HttpStatusCode.Created;

        return StatusCode((int)httpStatusCode, response);
    }

  
}