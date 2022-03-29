namespace SharedHome.Application.DTO
{
    public class ShoppingListDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public bool IsDone { get; set; }

        public IEnumerable<ShoppingListProductDto> Products { get; set; } = default!;
    }
}
