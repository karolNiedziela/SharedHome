namespace SharedHome.Application.ShoppingLists.DTO
{
    public class AddShoppingListProductDto
    {
        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public string? NetContent { get; set; }

        public int? NetContentType { get; set; } = default!;
    }
}
