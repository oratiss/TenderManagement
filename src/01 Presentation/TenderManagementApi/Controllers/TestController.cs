using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TenderManagementApi.Controllers
{
    public class TestController: ControllerBase
    {
        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult GetPublic()
        {
            return Ok("Public endpoint reached!");
        }

        [HttpGet("authorized")]
        [Authorize(Roles="admin")]
        public IActionResult GetAuthorized()
        {
            return Ok($"Authorized endpoint reached by user: {User.Identity.Name ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value}");
        }
    }
}
