using SharedHome.Domain.Bills.Constants;

namespace SharedHome.Application.Bills.DTO
{
    public class BillDto
    {
        public int Id { get; set; }

        public bool IsPaid { get; set; } = false;

        public BillType BillType { get; set; }

        public string ServiceProvider { get; set; } = default!;

        public decimal? Cost { get; set; }

        public string? Currency { get; set; }

        public DateTime DateOfPayment { get; set; }
    }
}
