using MediatR;
using NSubstitute;
using SharedHome.Application.Persons.Events;
using SharedHome.Domain.Persons.Events;
using SharedHome.Identity.Entities;
using SharedHome.Shared.Email.Senders;
using SharedHome.Tests.Shared.Stubs;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Persons
{
    public class PersonCreatedEventHandlerTests
    {
        private readonly UserManagerStub _userManagerStub;
        private readonly IIdentityEmailSender<ConfirmationEmailSender> _emailSender;
        private readonly INotificationHandler<PersonCreated> _notificationHandler;

        public PersonCreatedEventHandlerTests()
        {
            _userManagerStub = Substitute.For<UserManagerStub>();
            _emailSender = Substitute.For<IIdentityEmailSender<ConfirmationEmailSender>>();
            _notificationHandler = new PersonCreatedEventHandler(_userManagerStub, _emailSender);
        }

        [Fact]
        public async Task Handle_Should_Send_Email_To_Specific_Mailbox()
        {
            var personCreated = new PersonCreated(Guid.NewGuid(), "email@email.com");

            _userManagerStub.GenerateEmailConfirmationTokenAsync(Arg.Any<ApplicationUser>())
               .Returns("tokentokentoken");

            await _notificationHandler.Handle(personCreated, default!);


            await _emailSender.Received(1).SendAsync(Arg.Is<string>(x => x == "email@email.com"), Arg.Any<string>());
        }
    }
}
