using AutoMapper;
using SharedHome.Application.Bills.DTO;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Automapper.Profiles
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<BillReadModel, BillDto>()
                .ForMember(dest => dest.ServiceProvider, opt => opt.MapFrom(src => src.ServiceProviderName));
        }
    }
}
