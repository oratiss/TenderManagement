using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenderManagementApi.DTOs;
using TenderManagementService.AuthenticationServices;

namespace TenderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await authService.RegisterAsync(model.Email, model.Password);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await authService.LoginAsync(model.Email, model.Password);
            return Ok(new { token });
        }
    }
}
