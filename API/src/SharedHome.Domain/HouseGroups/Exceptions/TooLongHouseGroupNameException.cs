using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class TooLongHouseGroupNameException : SharedHomeException
    {
        public override string ErrorCode => throw new NotImplementedException();

        public override HttpStatusCode StatusCode => throw new NotImplementedException();

        [Order]
        public int MaxLength { get; }

        public TooLongHouseGroupNameException(int maxLength) : base($"Name cannot be longer than {maxLength} characters.")
        {
            MaxLength = maxLength;
        }
    }
}
