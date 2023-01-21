using MediatR;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.ShoppingLists.Events;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Application.ShoppingLists.Events
{
    public class ShoppingListCreatedEventHandler : INotificationHandler<ShoppingListCreated>
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;

        public ShoppingListCreatedEventHandler(IHouseGroupReadService houseGroupReadService, IAppNotificationService appNotificationService)
        {
            _houseGroupReadService = houseGroupReadService;
            _appNotificationService = appNotificationService;
        }

        public async Task Handle(ShoppingListCreated notification, CancellationToken cancellationToken)
        {
            if (!await _houseGroupReadService.IsPersonInHouseGroupAsync(notification.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupReadService.GetMemberPersonIdsExcludingCreatorAsync(notification.PersonId);

            foreach (var personId in personIds)
            {
                var notificationFields = new List<AppNotificationField>()
                {
                    new AppNotificationField(AppNotificationFieldType.Name, notification.ShoppingListName),
                    new AppNotificationField(AppNotificationFieldType.Target, TargetType.ShoppingList.ToString()),
                    new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Create.ToString())
                };
                var appNotification = new AppNotification(personId, nameof(ShoppingListCreated), fields: notificationFields, notification.PersonId);

                await _appNotificationService.AddAsync(appNotification);

                await _appNotificationService.BroadcastNotificationAsync(appNotification, personId, notification.PersonId);
            }
        }
    }
}
