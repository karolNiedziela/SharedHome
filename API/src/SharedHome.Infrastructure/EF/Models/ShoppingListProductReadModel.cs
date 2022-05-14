namespace SharedHome.Infrastructure.EF.Models
{
    internal class ShoppingListProductReadModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal? Price { get; set; }

        public bool IsBought { get; set; }

        public ShoppingListReadModel ShoppingList { get; set; } = default!;
    }
}
