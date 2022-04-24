using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Invitations.Commands
{
    public class DeleteInvitation : AuthorizeRequest, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }
    }
}
