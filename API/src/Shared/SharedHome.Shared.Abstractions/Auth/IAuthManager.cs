using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Auth
{
    public interface IAuthManager
    {
        AuthenticationSucessResult CreateToken(string userId, string firstName, string lastName, string email,
            string? role = null);
    }
}
