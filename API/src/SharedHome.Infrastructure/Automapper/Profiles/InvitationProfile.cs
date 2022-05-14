using AutoMapper;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Automapper.Profiles
{
    public class InvitationProfile : Profile
    {
        public InvitationProfile()
        {
            CreateMap<InvitationReadModel, InvitationDto>()
                .ForMember(dest => dest.SentByFirstName, opt => opt.MapFrom(src => src.RequestedByPerson.FirstName))
                .ForMember(dest => dest.SentByLastName, opt => opt.MapFrom(src => src.RequestedByPerson.LastName))
                .ForMember(dest => dest.InvitationStatus, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
