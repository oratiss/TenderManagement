using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Mapster;
using TenderManagementApi.DTOs;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementService.TenderServices;
using TenderManagementService.TenderServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TenderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendersController(ITenderService tenderService) : ControllerBase
    {
        private readonly ITenderService _tenderService = tenderService;

        // GET: api/<TendersController>
        [HttpGet("Get")]
        public ActionResult<ApiResponse<GetAllResponse?>> GetAll()
        {
            ApiResponse<GetAllResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            var (tenders, count) = _tenderService.GetAllTenders(new GetTendersServiceRequest()
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

        // GET api/<TendersController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<GetTenderResponse?>> Get(int id)
        {
            ApiResponse<GetTenderResponse> response = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            var existingTender = _tenderService.GetTender(id);
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

        // POST api/<TendersController>
        [HttpPost]
        public void Post([FromBody] AddTenderDto addTenderRequest)
        {
        }

        // PUT api/<TendersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TendersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
