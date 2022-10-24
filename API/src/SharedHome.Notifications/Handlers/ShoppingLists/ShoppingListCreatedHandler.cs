using MediatR;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.Events;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Notifications.Handlers.ShoppingLists
{
    public class ShoppingListCreatedHandler : INotificationHandler<DomainEventNotification<ShoppingListCreated>>
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;

        public ShoppingListCreatedHandler(IHouseGroupReadService houseGroupReadService, IAppNotificationService appNotificationService)
        {
            _houseGroupReadService = houseGroupReadService;
            _appNotificationService = appNotificationService;
        }

        public async Task Handle(DomainEventNotification<ShoppingListCreated> notification, CancellationToken cancellationToken)
        {
            var shoppingListCreated = notification.DomainEvent;

            if (!await _houseGroupReadService.IsPersonInHouseGroup(shoppingListCreated.Creator.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupReadService.GetMemberPersonIdsExcludingCreator(shoppingListCreated.Creator.PersonId);

            foreach (var personId in personIds)
            {
                var notificationFields = new List<AppNotificationField>()
                {
                    new AppNotificationField(AppNotificationFieldType.Name, shoppingListCreated.ShoppingListName),
                    new AppNotificationField(AppNotificationFieldType.Target, TargetType.ShoppingList.ToString()),
                    new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Create.ToString())
                };
                var appNotification = new AppNotification(personId, nameof(ShoppingListCreated), fields: notificationFields);

                await _appNotificationService.AddAsync(appNotification);

                await _appNotificationService.BroadcastNotificationAsync(appNotification, personId, shoppingListCreated.Creator.PersonId);
            }           
        }
    }
}
