namespace SharedHome.Application.ShoppingLists.DTO
{
    public class ShoppingListDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public bool IsDone { get; set; }

        public string CreatedByFirstName { get; set; } = default!;

        public string CreatedByLastName { get; set; } = default!;

        public IEnumerable<ShoppingListProductDto> Products { get; set; } = default!;
    }
}
