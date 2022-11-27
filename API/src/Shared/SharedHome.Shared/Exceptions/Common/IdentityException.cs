using Microsoft.AspNetCore.Identity;

namespace SharedHome.Shared.Exceptions.Common
{
    public sealed class IdentityException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public IdentityException(IdentityResult result)
        {
            Errors = result.Errors.Where(x => x.Code != "DuplicateUserName").Select(error => error.Description); ;
        }
    }
}
