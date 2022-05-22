using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Invitations.Commands
{
    public class SendInvitation : IAuthorizeRequest, ICommand<Unit>
    {
        public string RequestedToPersonId { get; set; } = default!;

        public int HouseGroupId { get; set; }

        public string? PersonId { get; set; }
    }
}
