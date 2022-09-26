using MediatR;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.Events;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;
using SharedHome.Notifications.Services;

namespace SharedHome.Notifications.Handlers.ShoppingLists
{
    internal class AddShoppingListEventHandler : INotificationHandler<DomainEventNotification<ShoppingListCreated>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;

        public AddShoppingListEventHandler(INotificationRepository notificationRepository, IHouseGroupReadService houseGroupReadService)
        {
            _notificationRepository = notificationRepository;
            _houseGroupReadService = houseGroupReadService;
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
                var appNotification = new AppNotification(shoppingListCreated.Creator.PersonId, nameof(ShoppingListCreated), TargetType.ShoppingList, OperationType.Create);

                await _notificationRepository.AddAsync(appNotification);
            }            
        }
    }
}
