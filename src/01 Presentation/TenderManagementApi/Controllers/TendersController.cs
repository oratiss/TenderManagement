using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TenderManagementApi.Controllers.Abstractions;
using TenderManagementApi.DTOs;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.TenderServices;
using TenderManagementService.TenderServices.Models;

namespace TenderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendersController(ITenderService tenderService, IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
    {
        /// <summary>
        /// Returns All Tenders for admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,vendor")] // Use the role that works for your 'admin' token
        public ActionResult<ApiResponse<GetAllTendersResponse?>> GetAll()
        {
            ApiResponse<GetAllTendersResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            var (tenders, count) = tenderService.GetAllTenders(new GetTendersPaginatedServiceRequest()
            {
                SortBy = "Id",
                SortOrder = SortOrder.Asc
            });

            response.Data = new()
            {
                Tenders = tenders is null ? null : tenders!.Adapt<List<GetTenderResponse>>()
            };

            response.PaginationMetaData = tenders is null ? null : new PaginationMetaData
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



        /// <summary>
        // /// Gets a tender by its Id.
        // /// </summary>
        // /// <returns> a tender with given Id</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,vendor")]
        public ActionResult<ApiResponse<GetTenderResponse?>> Get([FromRoute] int id)
        {
            ApiResponse<GetTenderResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            var existingTender = tenderService.GetTender(id);
            if (existingTender is null)
            {
                response.Errors.Add(new Error()
                {
                    Code = 404,
                    ErrorMessage = "No Tender Found With Given Id"
                });
                httpStatusCode = HttpStatusCode.NotFound;
                return StatusCode((int)httpStatusCode, response);
            }

            response.Data = new()
            {
                Id = existingTender.Id,
                Title = existingTender.Title,
                Description = existingTender.Description,
                CategoryId = existingTender.CategoryId,
                CategoryName = existingTender.Category.Name,
                Deadline = existingTender.Deadline,
                StatusId = existingTender.StatusId,
                Status = existingTender.Status.Name
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

        /// <summary>
        /// adds a Tender
        /// </summary>
        /// <param name="addTenderRequest"></param>
        /// <returns>the added tender with its Id</returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<ApiResponse<AddTenderResponse>> Post([FromBody] AddTenderDto addTenderRequest)
        {
            ApiResponse<AddTenderResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            //todo: validate request

            var serviceResponse = tenderService.AddTender(addTenderRequest.Adapt<AddTenderServiceRequest>());
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

            response.Data = serviceResponse.Data.Adapt<AddTenderResponse>();
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

        /// <summary>
        /// Edits a Tender with given props of request. editing title should be not available in system.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>nothing returns  - 204 should be sent in case of success</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<ApiResponse<EditTenderResponse>> Put([FromRoute]int id, [FromBody] EditTenderDto request)
        {
            ApiResponse<EditTenderResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            //todo: validate request

            var editServiceRequest = request.Adapt<EditTenderServiceRequest>();
            editServiceRequest.Id = id;
            var serviceResponse = tenderService.EditTender(editServiceRequest, CurrentUserId!);
            if (serviceResponse.Errors!.Any())
            {
                switch (serviceResponse.Errors!.First().Code)
                {
                    case 400:
                        httpStatusCode = HttpStatusCode.BadRequest;
                        break;
                    case 404:
                        httpStatusCode = HttpStatusCode.NotFound;
                        break;
                    case 500:
                        httpStatusCode = HttpStatusCode.InternalServerError;
                        break;
                }

                serviceResponse.Errors = [];
                serviceResponse.Errors.ForEach(e => response.Errors.Add(e.Adapt<Error>()));
                return StatusCode((int)httpStatusCode, response);
            }

            //no data will be returned cause we want to use no content response
            response.Succeeded = true;
            httpStatusCode = HttpStatusCode.NoContent;

            return StatusCode((int)httpStatusCode, response);
        }

        /// <summary>
        /// Deletes a tender by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>nothing returns  - 204 should be sent in case of success</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<ApiResponse<DeleteTenderResponse>> Delete([FromRoute]int id)
        {
            ApiResponse<EditTenderResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            var serviceResponse = tenderService.DeleteTender(id, CurrentUserId!);
            if (serviceResponse!.Errors!.Any())
            {
                switch (serviceResponse.Errors!.First().Code)
                {
                    case 404:
                        httpStatusCode = HttpStatusCode.NotFound;
                        break;
                    case 500:
                        httpStatusCode = HttpStatusCode.InternalServerError;
                        break;
                }

                serviceResponse.Errors = [];
                serviceResponse.Errors.ForEach(e => response.Errors.Add(e.Adapt<Error>()));
                return StatusCode((int)httpStatusCode, response);
            }

            //no data will be returned cause we want to use no content response
            response.Succeeded = true;
            httpStatusCode = HttpStatusCode.NoContent;

            return StatusCode((int)httpStatusCode, response);
        }
    }
}
