using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
                var userClaim = httpContext?.User.FindFirst("UserId");
                if (userClaim != null && string.IsNullOrWhiteSpace(userClaim!.Value))
                {
                    return HttpContext.User.FindFirst("UserId")!.Value;
                }
                return null;
            }
        }
    }
}
