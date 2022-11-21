using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedHome.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;
using System.Security.Claims;

namespace SharedHome.Application.Common.User
{
    internal class CurrentUser : ICurrentUser
    {
        private readonly HttpContext _context;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext!;
        }

        public Guid UserId 
        { 
            get
            {
                var userId = _context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                return string.IsNullOrWhiteSpace(userId) ? Guid.Empty : new Guid(userId);
            }
        }

        public string FirstName
        {
            get
            {
                var firstName = _context?.User?.FindFirstValue(ClaimTypes.GivenName);

                return string.IsNullOrWhiteSpace(firstName) ? string.Empty : firstName;
            }
        }

        public string LastName
        {
            get
            {
                var lastName = _context?.User?.FindFirstValue(ClaimTypes.Surname);

                return string.IsNullOrWhiteSpace(lastName) ? string.Empty : lastName;
            }
        }

        public string Email
        {
            get
            {
                var email = _context?.User?.FindFirstValue(ClaimTypes.Email);

                return string.IsNullOrWhiteSpace(email) ? string.Empty : email;
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
