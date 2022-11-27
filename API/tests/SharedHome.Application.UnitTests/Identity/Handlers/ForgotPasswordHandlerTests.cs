using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SharedHome.Application.Identity.Commands.ForgotPassword;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;
using SharedHome.Shared.Email.Senders;
using SharedHome.Tests.Shared.Stubs;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Identity.Handlers
{
    public class ForgotPasswordHandlerTests
    {
        private readonly UserManagerStub _userManagerStub;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;
        private readonly IIdentityEmailSender<ForgotPasswordEmailSender> _emailSender;
        private readonly IRequestHandler<ForgotPasswordCommand, Unit> _commandHandler;

        public ForgotPasswordHandlerTests()
        {
            _userManagerStub = Substitute.For<UserManagerStub>();
            _logger = Substitute.For<ILogger<ForgotPasswordCommandHandler>>();
            _emailSender = Substitute.For<IIdentityEmailSender<ForgotPasswordEmailSender>>();
            _commandHandler = new ForgotPasswordCommandHandler(_userManagerStub, _logger, _emailSender);
        }

        [Fact]
        public async Task Handle_Should_Throw_InvalidCredentialsException_When_User_Not_Found()
        {
            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .ReturnsNull();

            var command = new ForgotPasswordCommand("email@test.com");

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidCredentialsException>();
        }

        [Fact]
        public async Task Handle_Should_Send_Email()
        {
            var applicationUser = new ApplicationUser
            {
                Email = "test@email.com"
            };

            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .Returns(applicationUser);

            _userManagerStub.GeneratePasswordResetTokenAsync(Arg.Any<ApplicationUser>())
                .Returns("tokentokentoken");

            var command = new ForgotPasswordCommand("email@test.com");

            var result = await _commandHandler.Handle(command, default);

            await _emailSender.Received(1).SendAsync(Arg.Is<string>(x => x == "test@email.com"), Arg.Any<string>());
            result.ShouldBe(Unit.Value);
        }
    }
}
