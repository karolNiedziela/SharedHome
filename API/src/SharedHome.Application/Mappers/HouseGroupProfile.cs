using AutoMapper;
using SharedHome.Application.DTO;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.ValueObjects;

namespace SharedHome.Application.Mappers
{
    public class HouseGroupProfile : Profile
    {
        public HouseGroupProfile()
        {
            CreateMap<HouseGroup, HouseGroupDto>();
            CreateMap<HouseGroupMember, HouseGroupMemberDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Value))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Value))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
        }
    }
}
