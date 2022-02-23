using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.User
{
    public interface ICurrentUser
    {
        bool IsAutenticated { get; }

        Guid? UserId { get; }


        string Role { get; }
    }
}
