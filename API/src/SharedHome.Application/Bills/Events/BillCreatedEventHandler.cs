using MediatR;
using SharedHome.Application.Common.Events;
using SharedHome.Application.ReadServices;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Application.Bills.Events
{
    public class BillCreatedEventHandler : INotificationHandler<DomainEventNotification<BillCreated>>
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;

        public BillCreatedEventHandler(IHouseGroupReadService houseGroupReadService, IAppNotificationService appNotificationService)
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
                    new AppNotificationField(AppNotificationFieldType.Target, TargetType.Bill.ToString()),
                    new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Create.ToString()),
                    new AppNotificationField(AppNotificationFieldType.DateOfPayment, billCreated.DateOfPayment.ToShortDateString())
                };

                var appNotification = new AppNotification(personId, nameof(BillCreated), notificationFields);

                await _appNotificationService.AddAsync(appNotification);

                await _appNotificationService.BroadcastNotificationAsync(appNotification, personId, billCreated.Creator.PersonId);
            }
        }
    }
}
