using AutoMapper;
using SharedHome.Application.Bills.DTO;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Automapper.Profiles
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<Bill, BillDto>()
                .ForMember(dest => dest.ServiceProvider, opt => opt.MapFrom(src => src.ServiceProvider.Name))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost!.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Cost!.Currency.Value));


            CreateMap<BillReadModel, BillDto>()
                .ForMember(dest => dest.ServiceProvider, opt => opt.MapFrom(src => src.ServiceProviderName));
        }
    }
}
