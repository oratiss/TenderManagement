using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace TenderManagementApi.Controllers.Abstractions
{
    [ApiController]
    public class BaseController(IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        protected string? CurrentUserId
        {
            get
            {
                var httpContext = httpContextAccessor.HttpContext;
                if (httpContext?.User == null)
                {
                    return null;
                }

                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                {
                    return userIdClaim.Value;
                }

                return null; // Return null if the NameIden
            }
        }
    }
}
