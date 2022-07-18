using AutoMapper;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Automapper.Profiles
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, ShoppingListDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Name));

            CreateMap<ShoppingListReadModel, ShoppingListDto>()
                .ForMember(dest => dest.CreatedByFirstName, opt => opt.MapFrom(src => src.Person.FirstName))          
                .ForMember(dest => dest.CreatedByLastName, opt => opt.MapFrom(src => src.Person.LastName));
            CreateMap<ShoppingListProductReadModel, ShoppingListProductDto>();
        }
    }
}
