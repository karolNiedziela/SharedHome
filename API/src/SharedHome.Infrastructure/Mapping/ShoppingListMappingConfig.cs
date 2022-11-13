using Mapster;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
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
                .Map(dest => dest.Price, src => src.Price == null ? null : new MoneyDto(src.Price.Amount, src.Price.Currency.Value))                ;

            config.NewConfig<ShoppingListReadModel, ShoppingListDto>();


            config.NewConfig<ShoppingListProductReadModel, ShoppingListProductDto>()
                .Map(dest => dest.NetContent, src => src.NetContent == null ? null : new NetContentDto(src.NetContent, src.NetContentType))                
                .Map(dest => dest.Price, src => src.Price == null ? null : new MoneyDto(src.Price.Value, src.Currency!));

            config.NewConfig<AddShoppingListProductDto, ShoppingListProduct>()
                .ConstructUsing(src => 
                ShoppingListProduct.Create(src.Name, 
                    new Quantity(src.Quantity), 
                    null,
                    src.NetContent == null || src.NetContent.NetContent == null ? null : new NetContent(src.NetContent.NetContent,
                        src.NetContent.NetContentType.HasValue ? 
                            EnumHelper.ToEnumByIntOrThrow<NetContentType>(src.NetContent.NetContentType.Value)
                            : null),
                    false,
                    Guid.NewGuid()
                    )
                );
        }
    }
}
