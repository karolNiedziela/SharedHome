using SharedHome.Domain.HouseGroups.Exceptions;

namespace SharedHome.Domain.HouseGroups.ValueObjects
{
    public record HouseGroupName
    {
        private const int MaxLength = 30;

        public string Value { get; } = default!;

        private HouseGroupName()
        {

        }

        public HouseGroupName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new HouseGroupNameEmptyException();
            }

            if (value.Length > MaxLength)
            {
                throw new TooLongHouseGroupNameException(MaxLength);
            }

            Value = value;
        }

        public static implicit operator string(HouseGroupName name) => name.Value;

        public static implicit operator HouseGroupName(string value) => new(value);
    }
}
