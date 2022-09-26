﻿using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Bills.Constants;

namespace SharedHome.Application.Bills.DTO
{
    public class BillDto : AuditableDto
    {
        public int Id { get; set; }

        public bool IsPaid { get; set; } = false;

        public string BillType { get; set; } = default!;

        public string ServiceProvider { get; set; } = default!;

        public MoneyDto? Cost { get; set; }

        public DateTime DateOfPayment { get; set; }
    }
}
