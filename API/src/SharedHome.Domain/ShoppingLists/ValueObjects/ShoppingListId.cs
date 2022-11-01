namespace SharedHome.Domain.ShoppingLists.ValueObjects
{
    public sealed record ShoppingListId
    {
        public Guid Value { get; }

        private ShoppingListId() : this(Guid.NewGuid())
        {

        }

        public ShoppingListId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(ShoppingListId id) => id.Value;

        public static implicit operator ShoppingListId(Guid id) => new(id);
    }
}
