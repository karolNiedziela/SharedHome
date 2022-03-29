using SharedHome.Shared.Abstractions.Exceptions;

namespace SharedHome.Application.HouseGroups.Exceptions
{
    public class PersonIsAlreadyInHouseGroupException : SharedHomeException
    {
        private string PersonId { get; }

        public PersonIsAlreadyInHouseGroupException(string personId) : base($"Person with id '{personId} is already in house group.")
        {
            PersonId = personId;
        }
    }
}
