using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Exceptions
{
    public class ValidatorException : Exception
    {
        public IEnumerable<string> ErrorMessages { get; }

        public ValidatorException(IEnumerable<string> errorMessages)
          : base("One or more validation errors occurred")
          => ErrorMessages = errorMessages;

    }
}
