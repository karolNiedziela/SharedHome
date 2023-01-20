using Mapster;
using SharedHome.Notifications.DTO;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Infrastructure.Mapping
{
    public class NotificationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AppNotification, AppNotificationDto>()
                .Map(dest => dest.Title, src => MapContext.Current.GetService<IAppNotificationInformationResolver>().GetTitle(src))
                .Map(dest => dest.Type, src => src.Type.ToString())
                .Map(dest => dest.CreatedByFullName, src => src.CreatedByFullName);
        }
    }
}
