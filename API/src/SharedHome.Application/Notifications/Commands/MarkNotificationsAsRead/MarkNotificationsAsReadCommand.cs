using MediatR;

using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Notifications.Commands.MarkNotificationsAsRead
{
    public class MarkNotificationsAsReadCommand : AuthorizeRequest, IRequest<Unit>
    {
    }
}
