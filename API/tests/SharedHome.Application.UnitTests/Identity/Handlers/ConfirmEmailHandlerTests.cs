using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SharedHome.Application.Authentication.Commands.ConfirmEmail;
using SharedHome.Tests.Shared.Stubs;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using SharedHome.Identity.Exceptions;
using SharedHome.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using SharedHome.Shared.Exceptions.Common;

namespace SharedHome.Application.UnitTests.Identity.Handlers
{
    public class ConfirmEmailHandlerTests
    {
        private readonly UserManagerStub _userManagerStub;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;
        private readonly IRequestHandler<ConfirmEmailCommand, Unit> _commandHandler;

        public ConfirmEmailHandlerTests()
        {
            _userManagerStub =  Substitute.For<UserManagerStub>();
            _logger = Substitute.For<ILogger<ConfirmEmailCommandHandler>>();
            _commandHandler = new ConfirmEmailCommandHandler(_userManagerStub, _logger);
        }

        [Fact]
        public async Task Handle_Should_Throw_InvalidCredentialExceptions_When_User_Not_Found()
        {
            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .ReturnsNullForAnyArgs();

            var command = new ConfirmEmailCommand("email@test.com", "code");

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidCredentialsException>();
        }

        [Fact]
        public async Task Handle_Should_Throw_EmailAlreadyConfirmed_When_User_Email_Is_Already_Confirmed()
        {
            var applicationUser = new ApplicationUser
            {
                EmailConfirmed = true
            };

            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .Returns(applicationUser);

            var command = new ConfirmEmailCommand("email@test.com", "code");

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmailAlreadyConfirmedException>();
        }

        [Fact]
        public async Task Handle_Should_Throw_IdentityException_When_IdentityResult_Failed()
        {
            var applicationUser = new ApplicationUser
            {
                EmailConfirmed = false
            };

            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .Returns(applicationUser);

            _userManagerStub.ConfirmEmailAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(Array.Empty<IdentityError>()));

            var command = new ConfirmEmailCommand("email@test.com", "code");

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<IdentityException>();
        }

        [Fact]
        public async Task Handle_Should_Throw_IdentityException_When_IdentityResult_Success()
        {
            var applicationUser = new ApplicationUser
            {
                EmailConfirmed = false
            };

            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .Returns(applicationUser);

            _userManagerStub.ConfirmEmailAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            var command = new ConfirmEmailCommand("email@test.com", "code");

            var result = await _commandHandler.Handle(command, default);

            result.ShouldBe(Unit.Value);
        }
    }
}
