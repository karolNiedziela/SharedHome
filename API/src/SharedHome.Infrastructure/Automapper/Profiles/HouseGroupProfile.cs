using AutoMapper;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Automapper.Profiles
{
    public class HouseGroupProfile : Profile
    {
        public HouseGroupProfile()
        {
            CreateMap<HouseGroup, HouseGroupDto>();
            CreateMap<HouseGroupMember, HouseGroupMemberDto>();

            CreateMap<HouseGroupReadModel, HouseGroupDto>();
            CreateMap<HouseGroupMemberReadModel, HouseGroupMemberDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Person.FirstName} {src.Person.LastName}"));
        }
    }
}
