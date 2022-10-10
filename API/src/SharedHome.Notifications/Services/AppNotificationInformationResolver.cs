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
        
        public AppNotificationInformationResolver(IStringLocalizerFactory localizerFactory, ILogger<AppNotificationInformationResolver> logger)
        {
            _titleLocalizer = localizerFactory.Create(Resources.NotificationTitle, Assembly.GetEntryAssembly()!.GetName().Name!);
            _messageLocalizer = localizerFactory.Create(Resources.NotificationMessage, Assembly.GetEntryAssembly()!.GetName().Name!);
            _logger = logger;
        }

        public string GetTitle(AppNotification appNotification, string? name = null)
        {
            var titleBuilder = new StringBuilder();

            AppendTarget(appNotification.Target, titleBuilder);
            if (!string.IsNullOrEmpty(name))
            {
                AppendConnectorWithName(appNotification.Target, titleBuilder, name);               
            }        
            AppendOperation(appNotification.Operation, titleBuilder);
            AppendBy(appNotification, titleBuilder);

            return titleBuilder.ToString();
        }

        public string GetMessage()
        {
            return string.Empty;
        }     

        private void AppendTarget(TargetType targetType, StringBuilder titleBuilder)
        {
            var targetTypeResourceStringValue = _titleLocalizer.GetString(targetType.ToString());
            if (targetTypeResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {target} not found.", targetType.ToString());
                return;
            }

            titleBuilder.Append(targetTypeResourceStringValue.Value);
            titleBuilder.Append(' ');
        }

        private void AppendConnectorWithName(TargetType targetType, StringBuilder titleBuilder, string name)
        {
            var connector = string.Empty;
            connector = targetType switch
            {
                TargetType.ShoppingList => "with name",
                TargetType.Bill => "with service provider name",
                TargetType.HouseGroup => "with name",
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

            titleBuilder.Append(connectorResourceString.Value);
            titleBuilder.Append($" {name}");
            titleBuilder.Append(' ');
        }

        private void AppendOperation(OperationType operationType, StringBuilder titleBuilder)
        {
            var operationTypeResourceStringValue = _titleLocalizer.GetString(operationType.ToString());
            if (operationTypeResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning("Resource {operation} not found.", operationType.ToString());
                return;
            }

            titleBuilder.Append(operationTypeResourceStringValue.Value);
            titleBuilder.Append(' ');
        }

        private void AppendBy(AppNotification appNotification, StringBuilder titleBuilder)
        {
            var byResourceStringValue = _titleLocalizer.GetString(Connectors.By);
            if (byResourceStringValue.ResourceNotFound)
            {
                _logger.LogWarning($"Resource {Connectors.By} not found.", Connectors.By);
                return;
            }

            titleBuilder.Append(byResourceStringValue.Value);


            switch (appNotification.Operation)
            {
                case OperationType.Create:
                    titleBuilder.Append($" {appNotification.CreatedBy}.");
                    return;
                
                case OperationType.Update:
                    titleBuilder.Append($" {appNotification.ModifiedBy}.");
                    break;

                case OperationType.Delete:
                    titleBuilder.Append($" {appNotification.ModifiedBy}.");
                    break;

                default:
                    return;
            }
        }
    }
}
