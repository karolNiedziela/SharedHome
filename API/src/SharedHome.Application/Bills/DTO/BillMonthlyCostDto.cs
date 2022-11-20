namespace SharedHome.Application.Bills.DTO
{
    public class BillMonthlyCostDto
    {
        public string MonthName { get; set; } = default!;

        public decimal? TotalCost { get; set; }

        public string Currency { get; set; } = default!;
    }
}
