using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class PersonIsNotInHouseGroupException : SharedHomeException
    {
        public override string ErrorCode => "PersonIsNotInHouseGroup";

        public PersonIsNotInHouseGroupException(string personId, int houseGroupId) 
            : base($"Person with id '{personId}' is not in house group with id '{houseGroupId}'")
        {
            PersonId = personId;
            HouseGroupId = houseGroupId;
        }

        public string PersonId { get; }
        public int HouseGroupId { get; }
    }
}
