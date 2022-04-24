using AutoMapper;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Domain.Invitations.Aggregates;

namespace SharedHome.Application.Mappers
{
    public class InvitationProfile : Profile
    {
        public InvitationProfile()
        {
            CreateMap<Invitation, InvitationDto>()
                .ForMember(dest => dest.SentByFirstName, opt => opt.MapFrom(src => src.SentByFirstName.Value))
                .ForMember(dest => dest.SentByLastName, opt => opt.MapFrom(src => src.SentByLastName.Value));
        }
    }
}
