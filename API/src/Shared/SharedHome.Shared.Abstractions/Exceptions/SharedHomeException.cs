using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Shared.Abstractions.Exceptions
{
    public abstract class SharedHomeException : Exception
    {
        protected SharedHomeException(string message) : base(message)
        {

        }
    }
}
