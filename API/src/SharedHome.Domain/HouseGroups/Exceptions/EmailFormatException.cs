using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class EmailFormatException : SharedHomeException
    {
        public EmailFormatException() : base($"Invalid email format.")
        {
        }
    }
}
