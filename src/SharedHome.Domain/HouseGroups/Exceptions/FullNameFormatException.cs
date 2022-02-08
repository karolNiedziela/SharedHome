using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class FullNameFormatException : SharedHomeException
    {
        public FullNameFormatException() : base($"Invalid full name format.")
        {
        }
    }
}
