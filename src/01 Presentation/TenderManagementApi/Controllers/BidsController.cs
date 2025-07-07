using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TenderManagementApi.Controllers.Abstractions;
using TenderManagementApi.DTOs;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementService.BidServices;
using TenderManagementService.BidServices.Models;

namespace TenderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController(IBidService bidService, IHttpContextAccessor httpContextAccessor) 
        : BaseController(httpContextAccessor)
    {
        /// <summary>
        /// Adds a new Bid
        /// </summary>
        /// <param name="addBidRequest"></param>
        /// <returns>added bid</returns>
        [HttpPost]
        [Authorize(Roles = "admin,vendor")]
        public ActionResult<ApiResponse<BidResponse>> Post([FromBody] AddBidDto addBidRequest)
        {
            ApiResponse<BidResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            //todo: validate request

            var serviceResponse = bidService.AddBid(addBidRequest.Adapt<AddBidServiceRequest>());
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

            response.Data = serviceResponse.Data.Adapt<BidResponse>();
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
        /// Edits a Bid with given props of request.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>nothing returns  - 204 should be sent in case of success</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<ApiResponse<BidResponse>> Put([FromRoute] long id, [FromBody] EditBidDto request)
        {
            ApiResponse<BidResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            //todo: validate request

            var editBidServiceRequest = request.Adapt<EditBidServiceRequest>();
            editBidServiceRequest.Id = id;
            var serviceResponse = bidService.EditBid(editBidServiceRequest, CurrentUserId!);
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

    }
}
