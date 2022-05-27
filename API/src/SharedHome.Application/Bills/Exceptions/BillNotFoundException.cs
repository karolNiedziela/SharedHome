﻿using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.Bills.Exceptions
{
    public class BillNotFoundException : SharedHomeException
    {
        public override string ErrorCode => "BillNotFound";

        public BillNotFoundException(int billId) : base($"Bill with id '{billId}' was not found.")
        {
        }
    }
}
