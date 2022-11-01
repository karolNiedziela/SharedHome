namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public sealed record ShoppingListProductId
    {
        public Guid Value { get; }

        public ShoppingListProductId() : this(Guid.NewGuid())
        {

        }

        public ShoppingListProductId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(ShoppingListProductId id) => id.Value;

        public static implicit operator ShoppingListProductId(Guid id) => new(id);
    }
}
