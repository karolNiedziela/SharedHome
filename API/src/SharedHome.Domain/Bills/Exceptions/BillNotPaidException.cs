using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillNotPaidException : SharedHomeException
    {
        public BillNotPaidException() : base($"Bill is not paid.")
        {
        }
    }
}
