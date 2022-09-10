using Mapster;
using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Mapping
{
    public class CommonMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MoneyDto, Money>().ConstructUsing(src => (src == null ? null : new Money(src.Price, src.Currency))!);

            config.NewConfig<NetContentDto, NetContent>().ConstructUsing(src => (src == null ? null : new NetContent(src.NetContent, src.NetContentType.HasValue ? (NetContentType)src.NetContentType : null))!);

            config.NewConfig<ShoppingListProductReadModel, NetContentDto>().ConstructUsing(src => (src.NetContent == null ? null : new NetContentDto(src.NetContent, src.NetContentType!.Value))!);
        }
    }
}
