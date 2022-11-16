using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class TooLongHouseGroupNameException : SharedHomeException
    {
        public override string ErrorCode => "TooLongHouseGroupName";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        [Order]
        public int MaxLength { get; }

        public TooLongHouseGroupNameException(int maxLength) 
            : base($"Name cannot be longer than '{maxLength}' characters.")
        {
            MaxLength = maxLength;
        }
    }
}
