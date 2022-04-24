using AutoMapper;
using SharedHome.Application.DTO;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.ValueObjects;

namespace SharedHome.Application.Mappers
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, ShoppingListDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Name));
            CreateMap<ShoppingListProduct, ShoppingListProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.Value))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price == null ? null : src.Price.Value));
        }
    }
}
