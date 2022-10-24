using MediatR;
using SharedHome.Application.Bills.Events;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Notifications.Handlers.Bills
{
    public class BillCreatedHandler : INotificationHandler<DomainEventNotification<BillCreated>>
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;

        public BillCreatedHandler(IHouseGroupReadService houseGroupReadService, IAppNotificationService appNotificationService)
        {
            _houseGroupReadService = houseGroupReadService;
            _appNotificationService = appNotificationService;
        }

        public async Task Handle(DomainEventNotification<BillCreated> notification, CancellationToken cancellationToken)
        {
            var billCreated = notification.DomainEvent;

            if (!await _houseGroupReadService.IsPersonInHouseGroup(billCreated.Creator.PersonId))
            {
                return;
            }

            var personIds = await _houseGroupReadService.GetMemberPersonIdsExcludingCreator(billCreated.Creator.PersonId);

            foreach (var personId in personIds)
            {
                var notificationFields = new List<AppNotificationField>()
                {
                    new AppNotificationField(AppNotificationFieldType.Name, billCreated.ServiceProviderName),
                    new AppNotificationField(AppNotificationFieldType.Target, TargetType.ShoppingList.ToString()),
                    new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Create.ToString())
                };

                var appNotification = new AppNotification(billCreated.Creator.PersonId, nameof(BillCreated), notificationFields);

                await _appNotificationService.AddAsync(appNotification);

                await _appNotificationService.BroadcastNotificationAsync(appNotification, personId, billCreated.Creator.PersonId);
            }
        }
    }
}
