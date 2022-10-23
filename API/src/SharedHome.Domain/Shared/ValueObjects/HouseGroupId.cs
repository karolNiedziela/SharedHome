namespace SharedHome.Domain.Shared.ValueObjects
{
    public record HouseGroupId
    {
        public Guid Value { get; }

        public HouseGroupId(): this(Guid.NewGuid())
        {

        }

        public HouseGroupId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(HouseGroupId houseGroupId) => houseGroupId.Value;

        public static implicit operator HouseGroupId(Guid value) => new(value);
    }
}
