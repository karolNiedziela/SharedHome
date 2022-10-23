using Mapster;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Common.DTO;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Mapping
{
    public class BillMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Bill, BillDto>()
                .Map(dest => dest.ServiceProvider, src => src.ServiceProvider.Name)
                .Map(dest => dest.BillType, src => src.BillType.ToString())
                .Map(dest => dest.DateOfPayment, src => src.DateOfPayment.ToDateTime(TimeOnly.MinValue))
                .Map(dest => dest.Cost, src => src.Cost == null ? null : new MoneyDto(src.Cost.Amount, src.Cost.Currency.Value));

            config.NewConfig<BillReadModel, BillDto>()
                .Map(dest => dest.ServiceProvider, src => src.ServiceProviderName)
                .Map(dest => dest.BillType, src => src.BillType.ToString())
                .Map(dest => dest.Cost, src => src.Cost == null ? null : new MoneyDto(src.Cost.Value, src.Currency!));
        }
    }
}
