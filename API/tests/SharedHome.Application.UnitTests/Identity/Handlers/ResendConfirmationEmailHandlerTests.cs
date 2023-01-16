using MediatR;
using NSubstitute;
using SharedHome.Application.Identity.Commands.ResendConfirmationEmail;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Email.Senders;
using SharedHome.Tests.Shared.Stubs;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Identity.Handlers
{
    public class ResendConfirmationEmailHandlerTests
    {
        private readonly UserManagerStub _userManagerStub;
        private readonly IIdentityEmailSender<ConfirmationEmailSender> _emailSender;
        private readonly IRequestHandler<ResendConfirmationEmailCommand, ApiResponse<string>> _commandHandler;

        public ResendConfirmationEmailHandlerTests()
        {
            _userManagerStub = Substitute.For<UserManagerStub>();
            _emailSender = Substitute.For<IIdentityEmailSender<ConfirmationEmailSender>>();
            _commandHandler = new ResendConfirmationEmailHandler(_userManagerStub, _emailSender);
        }

        [Fact]
        public async Task Handle_Should_Throw_InvalidCredentialsException_When_User_With_Given_Email_Does_Not_Exist()
        {
            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(new ResendConfirmationEmailCommand
            {
                Email = "email"
            }, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidCredentialsException>();
        }

        [Fact]
        public async Task Handle_Should_Send_Email_When_User_Exists()
        {
            var command = new ResendConfirmationEmailCommand
            {
                Email = "email"
            };

            _userManagerStub.FindByEmailAsync(Arg.Any<string>()).Returns(new ApplicationUser
            {
                Email = command.Email
            });

            await _commandHandler.Handle(command, default);

            await _emailSender.Received(1).SendAsync(Arg.Is<string>(x => x == command.Email), Arg.Any<string>());
        }
    }
}
