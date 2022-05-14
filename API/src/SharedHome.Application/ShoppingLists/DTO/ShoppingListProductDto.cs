namespace SharedHome.Application.ShoppingLists.DTO
{
    public class ShoppingListProductDto
    {
        public string Name { get; set; } = default!;

        public uint Quantity { get; set; }

        public decimal? Price { get; set; }

        public bool IsBought { get; set; }
    }
}
