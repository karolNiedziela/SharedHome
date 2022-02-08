using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class EmptyServiceProviderNameException : SharedHomeException
    {
        public EmptyServiceProviderNameException() : base($"Service provider name cannot be empty.")
        {
        }
    }
}
