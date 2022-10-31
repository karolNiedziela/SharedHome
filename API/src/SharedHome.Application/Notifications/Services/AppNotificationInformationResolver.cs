using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;
using SharedHome.Shared.Constants;
using System.Reflection;
using System.Text;

namespace SharedHome.Application.Notifications.Services
{
    public class AppNotificationInformationResolver : IAppNotificationInformationResolver
    {
        private readonly IStringLocalizer _titleLocalizer;
        private readonly IStringLocalizer _messageLocalizer;        
        private readonly ILogger<AppNotificationInformationResolver> _logger;
        private StringBuilder _titleBuilder = default!;
        private IEnumerable<AppNotificationField> _fields = new List<AppNotificationField>();

        private const string DateOfPaymentResourceString = "with date of payment";

        public AppNotificationInformationResolver(IStringLocalizerFactory localizerFactory, ILogger<AppNotificationInformationResolver> logger)
        {
            _titleLocalizer = localizerFactory.Create(Resources.NotificationTitle, Assembly.GetEntryAssembly()!.GetName().Name!);
            _messageLocalizer = localizerFactory.Create(Resources.NotificationMessage, Assembly.GetEntryAssembly()!.GetName().Name!);
            _logger = logger;
        }

        public string GetTitle(AppNotification appNotification)
        {
            _titleBuilder = new StringBuilder();
            _fields = appNotification.Fields;
            var targetField = GetAppNotificationFieldByType(AppNotificationFieldType.Target);
            AppendOperation(GetAppNotificationFieldByType(AppNotificationFieldType.Operation), targetField?.Value);
            AppendTarget(targetField);
            AppendName(GetAppNotificationFieldByType(AppNotificationFieldType.Name), targetField?.Value);
            AppendDateOfPayment(GetAppNotificationFieldByType(AppNotificationFieldType.DateOfPayment));

            _titleBuilder.Append('.');
            return _titleBuilder.ToString();
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
        
        private void AppendDateOfPayment(AppNotificationField? field)
        {
            if (field is null)
            {
                return;
            }

            var dateOfPaymentResourceString = _titleLocalizer.GetString(DateOfPaymentResourceString);
            if (dateOfPaymentResourceString.ResourceNotFound)
            {
                _logger.LogWarning("Resource {dateOfPayment} not found.", DateOfPaymentResourceString);
                return;
            }

            _titleBuilder.Append($" {dateOfPaymentResourceString.Value}");
            _titleBuilder.Append($" {field.Value}");
        }

        private AppNotificationField? GetAppNotificationFieldByType(AppNotificationFieldType fieldType)
            => _fields.FirstOrDefault(x => x.Type == fieldType);
    }
}
