using SharedHome.Application.Common.DTO;

namespace SharedHome.Application.ShoppingLists.DTO
{
    public class AddShoppingListProductDto
    {
        public string Name { get; set; } = default!;

        public int Quantity { get; set; }

        public NetContentDto? NetContent { get; set; }
    }
}
