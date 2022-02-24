using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Exceptions
{
    public sealed class IdentityException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public IdentityException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
