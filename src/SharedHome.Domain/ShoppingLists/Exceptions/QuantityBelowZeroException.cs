using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.ShoppingLists.Exceptions
{
    public class QuantityBelowZeroException : SharedHomeException
    {
        public QuantityBelowZeroException() : base("Quantity cannot be lower than zero.")
        {
        }
    }
}
