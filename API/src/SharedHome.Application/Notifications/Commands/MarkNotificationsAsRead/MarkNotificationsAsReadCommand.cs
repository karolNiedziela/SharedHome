using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Application.Common.Requests;

namespace SharedHome.Application.Notifications.Commands.MarkNotificationsAsRead
{
    public class MarkNotificationsAsReadCommand : AuthorizeRequest, ICommand<Unit>
    {
    }
}
