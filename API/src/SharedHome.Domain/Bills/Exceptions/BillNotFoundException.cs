using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.Bills.Exceptions
{
    public class BillNotFoundException : SharedHomeException
    {
        public BillNotFoundException(int billId) : base($"Bill with id '{billId}' was not found.")
        {
        }
    }
}
