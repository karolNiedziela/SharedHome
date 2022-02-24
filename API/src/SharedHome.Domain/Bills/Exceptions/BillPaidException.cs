using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillPaidException : SharedHomeException
    {
        public BillPaidException() : base($"Bill is already paid.")
        {
        }
    }
}
