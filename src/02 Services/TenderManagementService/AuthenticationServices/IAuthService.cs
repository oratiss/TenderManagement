using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenderManagementService.AuthenticationServices
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string email, string password, bool? isAdmin = null);
        Task<string> LoginAsync(string email, string password);
    }
}
