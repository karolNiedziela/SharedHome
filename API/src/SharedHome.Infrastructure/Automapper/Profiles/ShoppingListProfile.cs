using AutoMapper;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Automapper.Profiles
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingListReadModel, ShoppingListDto>()
                .ForMember(dest => dest.CreatedByFirstName, opt => opt.MapFrom(src => src.Person.FirstName))          
                .ForMember(dest => dest.CreatedByLastName, opt => opt.MapFrom(src => src.Person.LastName));
            CreateMap<ShoppingListProductReadModel, ShoppingListProductDto>();
        }
    }
}
