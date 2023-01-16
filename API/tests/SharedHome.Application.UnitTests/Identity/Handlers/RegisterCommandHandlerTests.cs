using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SharedHome.Application.Identity.Commands.Register;
using SharedHome.Domain.Persons;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Identity.Entities;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Email.Senders;
using SharedHome.Shared.Exceptions.Common;
using SharedHome.Tests.Shared.Stubs;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Identity.Handlers
{
    public class RegisterCommandHandlerTests
    {
        private readonly UserManagerStub _userManagerStub;
        private readonly IIdentityEmailSender<ConfirmationEmailSender> _emailSender;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IRequestHandler<RegisterCommand, ApiResponse<string>> _commandHandler;

        public RegisterCommandHandlerTests()
        {
            _userManagerStub = Substitute.For<UserManagerStub>();           
            _emailSender = Substitute.For<IIdentityEmailSender<ConfirmationEmailSender>>();
            _personRepository = Substitute.For<IPersonRepository>();
            _logger = Substitute.For<ILogger<RegisterCommandHandler>>();
            _commandHandler = new RegisterCommandHandler(_userManagerStub, _emailSender, _personRepository, _logger);
        }

        [Fact]
        public async Task Handle_Should_Throw_IdentityException_When_Result_UnSucceeded()
        {
            var command = new RegisterCommand(
              "email@test.com",
              "Test",
              "LastName",
              "Password123");

            _userManagerStub.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(Array.Empty<IdentityError>()));

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<IdentityException>();
        }

        [Fact]
        public async Task Handle_Should_Create_Person_And_Send_Confirm_Email_When_RequireConfirmedEmailOption_Is_True()
        {
            var command = new RegisterCommand(
              "email@test.com",
              "Test",
              "LastName",
              "Password123");

            _userManagerStub.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            _userManagerStub.Options.SignIn.RequireConfirmedEmail= true;

            _userManagerStub.GenerateEmailConfirmationTokenAsync(Arg.Any<ApplicationUser>())
                .Returns("tokentokentoken");

            var response = await _commandHandler.Handle(command, default);

            await _personRepository.Received(1).AddAsync(Arg.Is<Person>(x => x.FirstName == "Test" &&
            x.LastName == "LastName"));

            await _emailSender.Received(1).SendAsync(Arg.Is<string>(x => x == "email@test.com"), Arg.Any<string>());

            response.Data.ShouldBe("Thanks for signing up. Check your mailbox and confirm email to get fully access.");
        }

        [Fact]
        public async Task Handle_Should_Create_Person_And_Not_Send_Confirm_Email_When_RequireConfirmedEmailOption_Is_False()
        {
            var command = new RegisterCommand(
              "email@test.com",
              "Test",
              "LastName",
              "Password123");

            _userManagerStub.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            _userManagerStub.Options.SignIn.RequireConfirmedEmail = false;

            _userManagerStub.GenerateEmailConfirmationTokenAsync(Arg.Any<ApplicationUser>())
                .Returns("tokentokentoken");

            var response = await _commandHandler.Handle(command, default);

            await _personRepository.Received(1).AddAsync(Arg.Is<Person>(x => x.FirstName == "Test" &&
            x.LastName == "LastName"));

            await _emailSender.Received(0).SendAsync(Arg.Any<string>(), Arg.Any<string>());

            response.Data.ShouldBe("Thanks for signing up.");
        }
    }
}
