using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Notifications.Commands.MarkNotificationsAsRead
{
    public class MarkNotificationsAsReadCommand : AuthorizeRequest, ICommand<Unit>
    {
    }
}
