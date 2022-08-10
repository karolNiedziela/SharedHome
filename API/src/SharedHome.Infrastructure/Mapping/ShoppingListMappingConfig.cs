using Mapster;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Helpers;

namespace SharedHome.Infrastructure.Mapping
{
    public class ShoppingListMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ShoppingList, ShoppingListDto>()
                .Map(dest => dest.Name, src => src.Name.Name);

            config.NewConfig<ShoppingListProduct, ShoppingListProductDto>()
                .Map(dest => dest.Name, src => src.Name.Value)
                .Map(dest => dest.Quantity, src => src.Quantity.Value)
                .Map(dest => dest.Price, src => src.Price == null ? null : (decimal?)src.Price!.Amount)
                .Map(dest => dest.Currency, src => src.Price == null ? null : src.Price!.Currency)
                .Map(dest => dest.NetContent, src => src.NetContent == null ? null : src.NetContent.Value)
                .Map(dest => dest.NetContentType, src => src.NetContent == null ? null : src.NetContent.Type.ToString());

            config.NewConfig<ShoppingListReadModel, ShoppingListDto>()
                .Map(dest => dest.CreatedByFirstName, src => src.Person.FirstName)
                .Map(dest => dest.CreatedByLastName, src => src.Person.LastName)
                .Map(dest => dest.CreatedByFullName, src => $"{src.Person.FirstName} {src.Person.LastName}");

            config.NewConfig<ShoppingListProductReadModel, ShoppingListProductDto>()
                .Map(dest => dest.NetContentType, src => src.NetContentType == null ? null : src.NetContentType!.ToString());

            config.NewConfig<AddShoppingListProductDto, ShoppingListProduct>()
                .ConstructUsing(src => 
                new ShoppingListProduct(src.Name, 
                    src.Quantity, 
                    null,
                    new NetContent(src.NetContent, 
                        src.NetContentType.HasValue ?
                        EnumHelper.ToEnumByIntOrThrow<NetContentType>(src.NetContentType.Value) :
                        null),
                    false
                    )
                );
        }
    }
}
