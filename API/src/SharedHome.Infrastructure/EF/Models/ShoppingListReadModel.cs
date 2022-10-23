namespace SharedHome.Infrastructure.EF.Models
{
    internal class ShoppingListReadModel : BaseReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public bool IsDone { get; set; }

        public ICollection<ShoppingListProductReadModel> Products { get; set; } = default!;

        public PersonReadModel Person { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;
    }
}
