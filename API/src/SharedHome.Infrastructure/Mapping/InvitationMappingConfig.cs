﻿using Mapster;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Domain.Invitations;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Mapping
{
    public class InvitationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Invitation, InvitationDto>()
                .Map(dest => dest.InvitationStatus, src => src.Status.ToString());

            config.NewConfig<InvitationReadModel, InvitationDto>()
                .Map(dest => dest.HouseGroupName, src => src.HouseGroup.Name)
                .Map(dest => dest.SentByFirstName, src => src.RequestedByPerson.FirstName)
                .Map(dest => dest.SentByLastName, src => src.RequestedByPerson.LastName)
                .Map(dest => dest.SentByFullName, src => $"{src.RequestedByPerson.FirstName} {src.RequestedByPerson.LastName}")
                .Map(dest => dest.InvitationStatus, src => src.Status.ToString());
        }
    }
}
