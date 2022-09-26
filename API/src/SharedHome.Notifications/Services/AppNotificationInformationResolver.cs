using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Shared.Constants;
using System.Reflection;
using System.Text;

namespace SharedHome.Notifications.Services
{
    public class AppNotificationInformationResolver : IAppNotificationInformationResolver
    {
        private const string By = "by";

        private readonly IStringLocalizer _titleLocalizer;
        private readonly IStringLocalizer _messageLocalizer;        
        private readonly ILogger<AppNotificationInformationResolver> _logger;
        

        public AppNotificationInformationResolver(IStringLocalizerFactory localizerFactory, ILogger<AppNotificationInformationResolver> logger)
        {
            _titleLocalizer = localizerFactory.Create(Resources.NotificationTitle, Assembly.GetEntryAssembly()!.GetName().Name!);
            _messageLocalizer = localizerFactory.Create(Resources.NotificationMessage, Assembly.GetEntryAssembly()!.GetName().Name!);
            _logger = logger;
        }

        public string GetTitle(AppNotification appNotification)
        {
            var titleBuilder = new StringBuilder();

            var targetTypeResourceStringValue = _titleLocalizer.GetString(appNotification.Target.ToString());
            if (targetTypeResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {target} not found.", appNotification.Target.ToString());
                return string.Empty;
            }

            titleBuilder.Append(targetTypeResourceStringValue.Value);
            titleBuilder.Append(' ');

            var operationTypeResourceStringValue = _titleLocalizer.GetString(appNotification.Operation.ToString());
            if (operationTypeResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {operation} not found.", appNotification.Operation.ToString());
                return titleBuilder.ToString();
            }

            titleBuilder.Append(operationTypeResourceStringValue.Value);

            var byResourceStringValue = _titleLocalizer.GetString(By);
            if (byResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {by} not found.", By);
                return titleBuilder.ToString();
            }

            titleBuilder.Append(' ');
            titleBuilder.Append(byResourceStringValue.Value);

            if (appNotification.Operation == OperationType.Create)
            {
                titleBuilder.Append($" {appNotification.CreatedBy}.");
            }
            else
            {
                titleBuilder.Append($" {appNotification.ModifiedBy}.");
            }


            return titleBuilder.ToString();
        }

        public string GetMessage()
        {
            return string.Empty;
        }      
    }
}
