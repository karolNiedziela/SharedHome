using MediatR;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Bills.Events;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Application.Bills.Events
{
    public class BillCreatedEventHandler : INotificationHandler<BillCreated>
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IAppNotificationService _appNotificationService;

        public BillCreatedEventHandler(IHouseGroupReadService houseGroupReadService, IAppNotificationService appNotificationService)
        {
            _houseGroupReadService = houseGroupReadService;
            _appNotificationService = appNotificationService;
        }       

        public async Task Handle(BillCreated notification, CancellationToken cancellationToken)
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
                    new AppNotificationField(AppNotificationFieldType.Name, notification.ServiceProviderName),
                    new AppNotificationField(AppNotificationFieldType.Target, TargetType.Bill.ToString()),
                    new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Create.ToString()),
                    new AppNotificationField(AppNotificationFieldType.DateOfPayment, notification.DateOfPayment.ToShortDateString())
                };

                var appNotification = new AppNotification(personId, nameof(BillCreated), notificationFields, notification.PersonId);

                await _appNotificationService.AddAsync(appNotification);

                await _appNotificationService.BroadcastNotificationAsync(appNotification, personId, notification.PersonId);
            }
        }
    }
}
