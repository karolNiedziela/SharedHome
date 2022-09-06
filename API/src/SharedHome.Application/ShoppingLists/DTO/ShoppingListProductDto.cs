using SharedHome.Application.Common.DTO;

namespace SharedHome.Application.ShoppingLists.DTO
{
    public class ShoppingListProductDto
    {
        public string Name { get; set; } = default!;

        public uint Quantity { get; set; }

        public MoneyDto? Price { get; set; }

        public string? NetContent { get; set; }

        public string? NetContentType { get; set; }

        public bool IsBought { get; set; }
    }
}
