using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Shared.Exceptions
{
    public class MoneyBelowZeroException : SharedHomeException
    {
        public MoneyBelowZeroException() : base("Amount of money cannot be lower than zero.")
        {
        }
    }
}
