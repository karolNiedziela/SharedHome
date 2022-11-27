using MediatR;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using SharedHome.Application.Identity.Commands.ChangePassword;
using SharedHome.Identity.Entities;
using SharedHome.Shared.Exceptions.Common;
using SharedHome.Tests.Shared.Stubs;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Identity.Handlers
{
    public class ChangePasswordHandlerTests
    {
        private readonly UserManagerStub _userManagerStub;
        private readonly IRequestHandler<ChangePasswordCommand, Unit> _commandHandler;

        public ChangePasswordHandlerTests()
        {
            _userManagerStub = Substitute.For<UserManagerStub>();
            _commandHandler = new ChangePasswordCommandHandler(_userManagerStub);
        }

        [Fact]
        public async Task Handle_Should_Throw_IdentityException_When_Result_Failed()
        {
            _userManagerStub.FindByIdAsync(Arg.Any<string>())
                .Returns(new ApplicationUser());

            _userManagerStub.ChangePasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(Array.Empty<IdentityError>()));

            var command = new ChangePasswordCommand();

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<IdentityException>();
        }

        [Fact]
        public async Task Handle_Should_Return_Unit_Value_When_IdentityResult_Success()
        {
            _userManagerStub.FindByIdAsync(Arg.Any<string>())
                .Returns(new ApplicationUser());

            _userManagerStub.ChangePasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            var command = new ChangePasswordCommand();

            var result = await _commandHandler.Handle(command, default);

            result.ShouldBe(Unit.Value);
        }
    }
}
