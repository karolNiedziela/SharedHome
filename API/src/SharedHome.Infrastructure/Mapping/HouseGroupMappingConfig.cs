using Mapster;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Domain.HouseGroups.Aggregates;
using SharedHome.Domain.HouseGroups.Entities;
using SharedHome.Infrastructure.EF.Models;

namespace SharedHome.Infrastructure.Mapping
{
    public class HouseGroupMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<HouseGroup, HouseGroupDto>();

            config.NewConfig<HouseGroupMember, HouseGroupMemberDto>();

            config.NewConfig<HouseGroupReadModel, HouseGroupDto>();
            config.NewConfig<HouseGroupMemberReadModel, HouseGroupMemberDto>()
                .Map(dest => dest.FirstName, src => src.Person.FirstName)
                .Map(dest => dest.LastName, src => src.Person.LastName)
                .Map(dest => dest.FullName, src => $"{src.Person.FirstName} {src.Person.LastName}");
        }
    }
}
