using AutoMapper;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Domain.Invitations.Aggregates;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Abstractions.User;

namespace SharedHome.Infrastructure.Automapper.Profiles
{
    public class InvitationProfile : Profile
    {
        public InvitationProfile()
        {
            CreateMap<Invitation, InvitationDto>()
                .ForMember(dest => dest.HouseGroupId, opt => opt.MapFrom(src => src.HouseGroupId))
                .ForMember(dest => dest.RequestedByPersonId, opt => opt.MapFrom(src => src.RequestedByPersonId))
                .ForMember(dest => dest.RequestedToPersonId, opt => opt.MapFrom(src => src.RequestedToPersonId))
                .ForMember(dest => dest.InvitationStatus, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<InvitationReadModel, InvitationDto>()
                .ForMember(dest => dest.SentByFirstName, opt => opt.MapFrom(src => src.RequestedByPerson.FirstName))
                .ForMember(dest => dest.SentByLastName, opt => opt.MapFrom(src => src.RequestedByPerson.LastName))
                .ForMember(dest => dest.InvitationStatus, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
