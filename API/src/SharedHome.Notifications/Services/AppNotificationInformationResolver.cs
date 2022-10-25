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
        private readonly IStringLocalizer _titleLocalizer;
        private readonly IStringLocalizer _messageLocalizer;        
        private readonly ILogger<AppNotificationInformationResolver> _logger;
        private StringBuilder _titleBuilder = default!;
        
        public AppNotificationInformationResolver(IStringLocalizerFactory localizerFactory, ILogger<AppNotificationInformationResolver> logger)
        {
            _titleLocalizer = localizerFactory.Create(Resources.NotificationTitle, Assembly.GetEntryAssembly()!.GetName().Name!);
            _messageLocalizer = localizerFactory.Create(Resources.NotificationMessage, Assembly.GetEntryAssembly()!.GetName().Name!);
            _logger = logger;
        }

        public string GetTitle(AppNotification appNotification)
        {
            _titleBuilder = new StringBuilder();
            var targetField = appNotification.Fields.FirstOrDefault(x => x.Type == AppNotificationFieldType.Target);
            AppendOperation(appNotification.Fields.FirstOrDefault(x => x.Type == AppNotificationFieldType.Operation), targetField?.Value);
            AppendTarget(targetField);
            AppendName(appNotification.Fields.FirstOrDefault(x => x.Type == AppNotificationFieldType.Name), targetField?.Value);

            _titleBuilder.Append('.');
            return _titleBuilder.ToString();
        }


        public string GetMessage()
        {
            return string.Empty;
        }

        private void AppendOperation(AppNotificationField? field, string? targetType)
        {
            if (field is null)
            {
                return;
            }

            var targetAndOperationType = string.Join("", targetType, field.Value);
            var targetAndOperationTypeResourceStringValue = _titleLocalizer.GetString(targetAndOperationType);
            if (targetAndOperationTypeResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {targetAndOperationType} not found.", targetAndOperationType);
                return;
            }

            _titleBuilder.Append(targetAndOperationTypeResourceStringValue.Value);
            _titleBuilder.Append(' ');
        }

        private void AppendTarget(AppNotificationField? field)
        {
            if (field is null)
            {
                return;
            }
            var targetTypeResourceStringValue = _titleLocalizer.GetString(field.Value);
            if (targetTypeResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {target} not found.", field.Value);
                return;
            }

            _titleBuilder.Append(targetTypeResourceStringValue.Value);
            _titleBuilder.Append(' ');
        }

        private void AppendName(AppNotificationField? field, string? targetType)
        {
            if (field is null)
            {
                return;
            }

            var connector = string.Empty;
            connector = targetType switch
            {
                nameof(TargetType.ShoppingList) => "with name",
                nameof(TargetType.Bill) => "with service provider name",
                nameof(TargetType.HouseGroup) => "with name",
                _ => string.Empty,
            };

            if (string.IsNullOrEmpty(connector))
            {
                return;
            }

            var connectorResourceString = _titleLocalizer.GetString(connector);
            if (connectorResourceString.ResourceNotFound)
            {
                _logger.LogWarning("Resource {connector} not found.", connector);
                return;
            }

            _titleBuilder.Append(connectorResourceString.Value);
            _titleBuilder.Append($" {field.Value}");
        }      
    }
}
