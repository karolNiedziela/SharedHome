using SharedHome.Domain.Bills.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.Bills.DTO
{
    public class BillDto
    {
        public int Id { get; set; }

        public bool IsPaid { get; private set; } = false;

        public BillType BillType { get; private set; }

        public string ServiceProvider { get; private set; } = default!;

        public decimal? Cost { get; private set; }

        public DateTime DateOfPayment { get; private set; }
    }
}
