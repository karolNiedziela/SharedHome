using Mapster;
using SharedHome.Application.Bills.DTO;
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
                .Map(dest => dest.Cost, src => src.Cost == null ? null : (decimal?)src.Cost!.Amount)
                .Map(dest => dest.Currency, src => src.Cost == null ? null : src.Cost!.Currency);

            config.NewConfig<BillReadModel, BillDto>()
                .Map(dest => dest.ServiceProvider, src => src.ServiceProviderName);
        }
    }
}
