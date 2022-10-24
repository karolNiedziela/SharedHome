using Mapster;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;

namespace SharedHome.Infrastructure.Mapping
{
    public class NotificationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AppNotification, AppNotificationDto>()
                .Map(dest => dest.Type, src => src.Type.ToString());
        }
    }
}
