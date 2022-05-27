using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class PersonIsAlreadyInHouseGroupException : SharedHomeException
    {
        public override string ErrorCode => "PersonIsAlreadyInHouseGroup";

        public string PersonId { get; }

        public PersonIsAlreadyInHouseGroupException(string personId) : base($"Person with id '{personId} is already in house group.")
        {
            PersonId = personId;
        }
    }
}
