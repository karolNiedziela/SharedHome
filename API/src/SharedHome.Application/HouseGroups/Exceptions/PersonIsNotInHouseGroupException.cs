using SharedHome.Shared.Abstractions.Attributes;
using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class PersonIsNotInHouseGroupException : SharedHomeException
    {
        public override string ErrorCode => "PersonIsNotInHouseGroup";

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
