﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Domain.HouseGroups.ValueObjects
{
    public record HouseGroupMember
    {
        public Guid PersonId { get; }

        public FirstName FirstName { get; } = default!;

        public LastName LastName { get; } = default!;

        public Email Email { get; } = default!;

        public bool IsOwner { get; init; }

        public string FullName => $"{FirstName} {LastName}";

        private HouseGroupMember()
        {

        }

        public HouseGroupMember(Guid personId, FirstName firstName, LastName lastName, Email email, bool isOwner = false)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsOwner = isOwner;
        }
    }
}
