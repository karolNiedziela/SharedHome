using MediatR;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Requests;

namespace SharedHome.Application.Invitations.Commands
{
    public class AcceptInvitation : IAuthorizeRequest, ICommand<Unit>
    {
        public int HouseGroupId { get; set; }

        public string? PersonId { get; set; }
    }
}
