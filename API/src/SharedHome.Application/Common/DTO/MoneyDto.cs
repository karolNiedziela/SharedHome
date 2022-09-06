namespace SharedHome.Application.Common.DTO
{
    public class MoneyDto
    {
        public decimal Price { get; set; }

        public string Currency { get; set; } = default!;
    }
}
