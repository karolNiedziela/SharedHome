namespace SharedHome.Application.DTO
{
    public  class ShoppingListMonthlyCostDto
    {
        public string MonthName { get; set; } = default!;

        public decimal? TotalCost { get; set; }
    }
}
