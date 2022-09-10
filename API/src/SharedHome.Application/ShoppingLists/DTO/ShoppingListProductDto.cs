using SharedHome.Application.Common.DTO;

namespace SharedHome.Application.ShoppingLists.DTO
{
    public class ShoppingListProductDto
    {
        public string Name { get; set; } = default!;

        public uint Quantity { get; set; }

        public MoneyDto? Price { get; set; }

        public NetContentDto? NetContent { get; set; }

        public bool IsBought { get; set; }
    }
}
