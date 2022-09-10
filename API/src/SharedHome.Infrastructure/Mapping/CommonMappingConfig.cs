using Mapster;
using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Infrastructure.Mapping
{
    public class CommonMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MoneyDto, Money>().ConstructUsing(src => new Money(src.Price, src.Currency));
        }
    }
}
