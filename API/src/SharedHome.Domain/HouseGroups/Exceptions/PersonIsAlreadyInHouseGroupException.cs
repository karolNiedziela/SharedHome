using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class PersonIsAlreadyInHouseGroupException : SharedHomeException
    {
        public override string ErrorCode => "PersonIsAlreadyInHouseGroup";

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        [Order]
        public string PersonId { get; }

        public PersonIsAlreadyInHouseGroupException(string personId) : base($"Person with id '{personId}' is already in house group.")
        {
            PersonId = personId;
        }

    }
}
