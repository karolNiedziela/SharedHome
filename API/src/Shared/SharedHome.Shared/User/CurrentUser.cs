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

        public string UserId 
        { 
            get
            {
                var userId = _context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                return string.IsNullOrWhiteSpace(userId) ? string.Empty : userId;
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
