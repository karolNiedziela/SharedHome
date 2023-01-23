﻿using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Commands.SendInvitation;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Persons.Extensions;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Invitations;
using SharedHome.Domain.Invitations.Enums;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class SendInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IInvitationReadService _invitationService;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IPersonRepository _personRepository;
        private readonly IRequestHandler<SendInvitationCommand, Guid> _commandHandler;

        public SendInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _houseGroupService = Substitute.For<IHouseGroupReadService>();
            _invitationService = Substitute.For<IInvitationReadService>();
            _personRepository = Substitute.For<IPersonRepository>();
            _commandHandler = new SendInvitationHandler(_invitationRepository, _houseGroupService, _invitationService, _personRepository);
        }

        [Fact]
        public async Task Handle_Throws_PersonIsNotInHouseGroupException_When_Person_Is_Not_In_HouseGroup()
        {
            var person = PersonFakeProvider.Get();
            _personRepository.GetByEmailOrThrowAsync(Arg.Any<Email>())
                .Returns(person);

            _houseGroupService.IsPersonInHouseGroupAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(false);

            var exception = await Record.ExceptionAsync(() => 
                _commandHandler.Handle(new SendInvitationCommand(), default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PersonIsNotInHouseGroupException>();
        }

        [Fact]
        public async Task Handle_Throws_InvitationAlreadySentException_When_Invitation_Already_Sent_ToPerson()
        {
            var person = PersonFakeProvider.Get();
            _personRepository.GetByEmailOrThrowAsync(Arg.Any<Email>())
                .Returns(person);

            _houseGroupService.IsPersonInHouseGroupAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(true);

            _invitationService.IsAnyInvitationFromHouseGroupToPersonAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(true);

            var exception = await Record.ExceptionAsync(() =>
                _commandHandler.Handle(new SendInvitationCommand(), default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationAlreadySentException>();
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var person = PersonFakeProvider.Get();
            _personRepository.GetByEmailOrThrowAsync(Arg.Any<Email>())
                .Returns(person);

            _houseGroupService.IsPersonInHouseGroupAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
             .Returns(true);

            _invitationService.IsAnyInvitationFromHouseGroupToPersonAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(false);

            var command = new SendInvitationCommand
            {
                HouseGroupId = InvitationFakeProvider.HouseGroupId,
                PersonId = InvitationFakeProvider.RequestedToPersonId,
                RequestedToPersonEmail = "email@email.com",
            };

            var invitationId = await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).AddAsync(Arg.Is<Invitation>(invitation =>
                invitation.Status == InvitationStatus.Pending));

            await _invitationRepository.Received(1).AddAsync(Arg.Is<Invitation>(invitation =>
                invitation.Status == InvitationStatus.Sent));
        }
    }
}
