using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.Exceptions
{
    public class PersonIsAlreadyInHouseGroupException : SharedHomeException
    {
        public Guid PersonId { get; }

        public PersonIsAlreadyInHouseGroupException(Guid personId) : base($"Person with id '{personId}' is already in house group.")
        {
            PersonId = personId;
        }

    }
}
