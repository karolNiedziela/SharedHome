using MediatR;
using SharedHome.Application.Common.Events;
using SharedHome.Application.Notifications.Services;
using SharedHome.Application.ReadServices;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Application.ShoppingLists.Events
{
    public class ShoppingListCreatedEventHandler : INotificationHandler<DomainEventNotification<ShoppingListCreated>>
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;

        public ShoppingListCreatedEventHandler(IHouseGroupReadService houseGroupReadService, IAppNotificationService appNotificationService)
        {
            _houseGroupReadService = houseGroupReadService;
            _appNotificationService = appNotificationService;
        }

        public async Task Handle(DomainEventNotification<ShoppingListCreated> notification, CancellationToken cancellationToken)
        {
            var shoppingListCreated = notification.DomainEvent;

            if (!await _houseGroupReadService.IsPersonInHouseGroupAsync(shoppingListCreated.Creator.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupReadService.GetMemberPersonIdsExcludingCreatorAsync(shoppingListCreated.Creator.PersonId);

            var addNotificationTasks = new List<Task>();
            var broadcastNotificationTasks = new List<Task>();
            foreach (var personId in personIds)
            {
                var notificationFields = new List<AppNotificationField>()
                {
                    new AppNotificationField(AppNotificationFieldType.Name, shoppingListCreated.ShoppingListName),
                    new AppNotificationField(AppNotificationFieldType.Target, TargetType.ShoppingList.ToString()),
                    new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Create.ToString())
                };
                var appNotification = new AppNotification(personId, nameof(ShoppingListCreated), fields: notificationFields);              

                addNotificationTasks.Add(_appNotificationService.AddAsync(appNotification));
             
                broadcastNotificationTasks.Add(_appNotificationService.BroadcastNotificationAsync(appNotification, personId, shoppingListCreated.Creator.PersonId));
            }

            await Task.WhenAll(addNotificationTasks);
            await Task.WhenAll(broadcastNotificationTasks);
        }
    }
}
