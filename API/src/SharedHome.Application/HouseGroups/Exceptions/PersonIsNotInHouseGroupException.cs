using SharedHome.Shared.Attributes;
using SharedHome.Shared.Exceptions.Common;
using System.Net;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class PersonIsNotInHouseGroupException : SharedHomeException
    {
        public override string ErrorCode => "PersonIsNotInHouseGroup";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        [Order]
        public Guid PersonId { get; }

        [Order(1)]
        public Guid HouseGroupId { get; }

        public PersonIsNotInHouseGroupException(Guid personId, Guid houseGroupId) 
            : base($"Person with id '{personId}' is not in house group with id '{houseGroupId}'")
        {
            PersonId = personId;
            HouseGroupId = houseGroupId;
        }


    }
}
