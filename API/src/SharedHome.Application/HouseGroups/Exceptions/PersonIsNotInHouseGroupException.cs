using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;
using System.Net;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class PersonIsNotInHouseGroupException : SharedHomeException
    {
        public override string ErrorCode => "PersonIsNotInHouseGroup";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        [Order]
        public string PersonId { get; }

        [Order(1)]
        public int HouseGroupId { get; }

        public PersonIsNotInHouseGroupException(string personId, int houseGroupId) 
            : base($"Person with id '{personId}' is not in house group with id '{houseGroupId}'")
        {
            PersonId = personId;
            HouseGroupId = houseGroupId;
        }


    }
}
