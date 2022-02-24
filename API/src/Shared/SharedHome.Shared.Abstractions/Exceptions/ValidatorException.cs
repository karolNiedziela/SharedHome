using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Exceptions
{
    public class ValidatorException : SharedHomeException
    {
        public ValidatorException(IReadOnlyDictionary<string, string[]> errorsDictionary)
          : base("One or more validation errors occurred")
          => ErrorsDictionary = errorsDictionary;

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
