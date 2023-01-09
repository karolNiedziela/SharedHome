using MediatR;
using SharedHome.Application.Common.Events;
using SharedHome.Application.Invitations.Queries;
using SharedHome.Notifications.Constants;
using SharedHome.Notifications.Entities;
using SharedHome.Notifications.Services;

namespace SharedHome.Application.Invitations.Events
{
    public class InvitationSentHandler : INotificationHandler<DomainEventNotification<InvitationSent>>
    {
        private readonly IAppNotificationService _appNotificationService;

        public InvitationSentHandler(IAppNotificationService appNotificationService)
        {
            _appNotificationService = appNotificationService;
        }

        public async Task Handle(DomainEventNotification<InvitationSent> notification, CancellationToken cancellationToken)
        {
            var invitationSent = notification.DomainEvent;

            var notificationFields = new List<AppNotificationField>()
            {
                new AppNotificationField(AppNotificationFieldType.Target, TargetType.Invitation.ToString()),
                new AppNotificationField(AppNotificationFieldType.Operation, OperationType.Send.ToString()),
                new AppNotificationField(AppNotificationFieldType.SentBy, invitationSent.SentByFullName),
            };
            var appNotification = new AppNotification(invitationSent.RequestToPersonId, nameof(InvitationSent), notificationFields);
            await _appNotificationService.AddAsync(appNotification);

            await _appNotificationService.BroadcastNotificationAsync(appNotification, invitationSent.RequestToPersonId);
        }
    }
}
