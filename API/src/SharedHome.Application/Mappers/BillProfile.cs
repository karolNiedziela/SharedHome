using AutoMapper;
using SharedHome.Application.Bills.DTO;
using SharedHome.Domain.Bills.Entities;

namespace SharedHome.Application.Mappers
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<Bill, BillDto>()
                .ForMember(dest => dest.ServiceProvider, opt => opt.MapFrom(src => src.ServiceProvider.Name))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost == null ? null : src.Cost.Value));
        }
    }
}
