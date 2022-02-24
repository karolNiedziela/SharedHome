using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillCostBelowZeroException : SharedHomeException
    {
        public BillCostBelowZeroException() : base("Bill cost cannot be lower than zero.")
        {
        }
    }
}
