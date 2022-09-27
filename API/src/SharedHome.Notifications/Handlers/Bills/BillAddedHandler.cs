using MediatR;
using SharedHome.Application.Bills.Events;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Repositories;

namespace SharedHome.Notifications.Handlers.Bills
{
    public class BillAddedHandler : INotificationHandler<DomainEventNotification<BillAdded>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;

        public BillAddedHandler(INotificationRepository notificationRepository, IHouseGroupReadService houseGroupReadService)
        {
            _notificationRepository = notificationRepository;
            _houseGroupReadService = houseGroupReadService;
        }

        public async Task Handle(DomainEventNotification<BillAdded> notification, CancellationToken cancellationToken)
        {
            var shoppingListCreated = notification.DomainEvent;

            if (!await _houseGroupReadService.IsPersonInHouseGroup(shoppingListCreated.Creator.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupReadService.GetMemberPersonIdsExcludingCreator(shoppingListCreated.Creator.PersonId);

            foreach (var personId in personIds)
            {
                var appNotification = new AppNotification(shoppingListCreated.Creator.PersonId, nameof(BillAdded), TargetType.Bill, OperationType.Create);

                await _notificationRepository.AddAsync(appNotification);
            }
        }
    }
}
