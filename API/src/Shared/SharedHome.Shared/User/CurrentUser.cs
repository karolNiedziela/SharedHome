using Microsoft.AspNetCore.Http;
using SharedHome.Shared.Abstractions.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.User
{
    public class CurrentUser : ICurrentUser
    {
        private readonly HttpContext _context;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext!;
        }

        public Guid? UserId 
        { 
            get
            {
                var userId = _context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                return string.IsNullOrWhiteSpace(userId) ? Guid.Empty : Guid.Parse(userId);
            }
        }

        public bool IsAutenticated 
        { 
            get
            {
                return UserId.HasValue;
            }
        }

        public string Role
        {
            get
            {
                return _context.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role)?.Value!;
            }
        }


        public Dictionary<string, IEnumerable<string>> Claims
        {
            get
            {
                return _context.User.Claims.GroupBy(c => c.Type)
                    .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
            }
        }
    }
}
