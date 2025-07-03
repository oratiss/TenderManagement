using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TenderManagementApi.DTOs;
using TenderManagementApi.DTOs.Abstractions;
using TenderManagementApi.DTOs.Responses;
using TenderManagementDAL.Models;
using TenderManagementService.AuthenticationServices;

namespace TenderManagementApi.Controllers
{
    //Todo: add area for admin

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "vendor")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> Register([FromBody] RegisterDto model)
        {
            var response = new ApiResponse<RegisterResponse>();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            //todo: validate request

            var identityServiceResponse = await authService.RegisterAsync(model.Email, model.Password);
            if (string.IsNullOrWhiteSpace(identityServiceResponse))
            {
                response.Errors.Add(new Error()
                {
                    Code = 500,
                    ErrorMessage = $"خطایی رخ داده است"
                });
            }
            else
            {
                response.Data = new RegisterResponse() { UserId = identityServiceResponse };
                response.Succeeded = true;
                httpStatusCode = HttpStatusCode.OK;
            }

            return StatusCode((int)httpStatusCode, response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginDto model)
        {
            var response = new ApiResponse<LoginResponse>();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            //todo: validate request

            var identityServiceResponse = await authService.LoginAsync(model.Email, model.Password);
            if (string.IsNullOrWhiteSpace(identityServiceResponse))
            {
                response.Errors.Add(new Error()
                {
                    Code = 500,
                    ErrorMessage = $"خطایی رخ داده است"
                });
            }
            else
            {
                response.Data = new LoginResponse() { Token = identityServiceResponse };
                response.Succeeded = true;
                httpStatusCode = HttpStatusCode.OK;
            }

            return StatusCode((int)httpStatusCode, response);
        }
    }
}
