using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Shared.Helpers
{
    public static class EnumHelper
    {
        public static TEnum ToEnumByIntOrThrow<TEnum>(int value)
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)Enum.ToObject(typeof(TEnum), value);
            }

            throw new InvalidEnumException();
        }
    }
}
