using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SharedHome.Application.Authentication.Queries.Login;
using SharedHome.Identity.Authentication;
using SharedHome.Identity.Authentication.Services;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;
using SharedHome.Tests.Shared.Stubs;
using Shouldly;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Identity.Handlers
{
    public class LoginHandlerTests
    {
        private readonly UserManagerStub _userManagerStub;
        private readonly IAuthManager _authManager;
        private readonly IRequestHandler<LoginQuery, AuthenticationResponse> _queryHandler;

        public LoginHandlerTests()
        {
            _userManagerStub = Substitute.For<UserManagerStub>();
            _authManager= Substitute.For<IAuthManager>();
            _queryHandler = new LoginQueryHandler(_userManagerStub, _authManager);
        }

        [Fact]
        public async Task Handle_Should_Throw_InvalidCredentialsException_When_User_Not_Found()
        {
            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .ReturnsNull();

            var command = new LoginQuery("test@email.com", "testPassword");
            var exception = await Record.ExceptionAsync(() => _queryHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidCredentialsException>();
        }

        [Fact]
        public async Task Handle_Should_Throw_InvalidCredentialsException_When_Password_Is_Invalid()
        {
            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
               .Returns(new ApplicationUser());

            _userManagerStub.CheckPasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(false);

            var command = new LoginQuery("test@email.com", "testPassword");
            var exception = await Record.ExceptionAsync(() => _queryHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidCredentialsException>();
        }

        [Fact]
        public async Task Handle_Should_Throw_EmailNotConfirmedException_When_User_Did_Not_Confirm_Email()
        {
            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
                .Returns(new ApplicationUser()
                {
                    EmailConfirmed = false
                });

            _userManagerStub.CheckPasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(true);

            _userManagerStub.Options.SignIn.RequireConfirmedAccount = true;
            _userManagerStub.Options.SignIn.RequireConfirmedEmail = true;

            var command = new LoginQuery("test@email.com", "testPassword");
            var exception = await Record.ExceptionAsync(() => _queryHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmailNotConfirmedException>();
        }

        [Fact]
        public async Task Handle_Should_Return_AuthenticationResponse()
        {

            _userManagerStub.FindByEmailAsync(Arg.Any<string>())
               .Returns(new ApplicationUser() 
               {
                   EmailConfirmed = true
               });

            _userManagerStub.CheckPasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(true);

            _userManagerStub.GetRolesAsync(Arg.Any<ApplicationUser>())
              .Returns(new string[] { "Role" });

            _authManager.Authenticate(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<IEnumerable<string>>())
                .Returns(new AuthenticationResponse
                {
                    AccessToken = "asdsadsa"
                });

            var command = new LoginQuery("test@email.com", "testPassword");

            var response = await _queryHandler.Handle(command, default);

            response.AccessToken.ShouldBe("asdsadsa");
        }
    }
}
