using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Infrastructure.Identity.Exceptions
{
    public class InvalidRefreshTokenException : SharedHomeException
    {
        public InvalidRefreshTokenException() : base("Refresh token was invalid.")
        {
        }
    }
}
