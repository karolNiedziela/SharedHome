﻿using Mapster;
using MapsterMapper;
using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Exceptions;
using SharedHome.Application.Invitations.Commands.SendInvitation;
using SharedHome.Application.Invitations.Dto;
using SharedHome.Application.Invitations.Exceptions;
using SharedHome.Application.Persons.Extensions;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Common.Events;
using SharedHome.Domain.Invitations;
using SharedHome.Domain.Invitations.Enums;
using SharedHome.Domain.Invitations.Repositories;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Domain.Persons.ValueObjects;
using SharedHome.Infrastructure;

using SharedHome.Shared.Application.Responses;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Invitations.Handlers
{
    public class SendInvitationHandlerTests
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IInvitationReadService _invitationService;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IRequestHandler<SendInvitationCommand, Response<InvitationDto>> _commandHandler;

        public SendInvitationHandlerTests()
        {
            _invitationRepository = Substitute.For<IInvitationRepository>();
            _houseGroupService = Substitute.For<IHouseGroupReadService>();
            _invitationService = Substitute.For<IInvitationReadService>();
            _personRepository = Substitute.For<IPersonRepository>();
            _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();
            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetAssembly(typeof(InfrastructureAssemblyReference))!);
            _mapper = new Mapper(config);
            _commandHandler = new SendInvitationHandler(_invitationRepository, _houseGroupService, _invitationService, _mapper, _personRepository, _domainEventDispatcher);
        }

        [Fact]
        public async Task Handle_Throws_PersonIsNotInHouseGroupException_When_Person_Is_Not_In_HouseGroup()
        {
            var person = PersonProvider.Get();
            _personRepository.GetByEmailOrThrowAsync(Arg.Any<Email>())
                .Returns(person);

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(false);

            var exception = await Record.ExceptionAsync(() => 
                _commandHandler.Handle(new SendInvitationCommand(), default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PersonIsNotInHouseGroupException>();
        }

        [Fact]
        public async Task Handle_Throws_InvitationAlreadySentException_When_Invitation_Already_Sent_ToPerson()
        {
            var person = PersonProvider.Get();
            _personRepository.GetByEmailOrThrowAsync(Arg.Any<Email>())
                .Returns(person);

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(true);

            _invitationService.IsAnyInvitationFromHouseGroupToPerson(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(true);

            var exception = await Record.ExceptionAsync(() =>
                _commandHandler.Handle(new SendInvitationCommand(), default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvitationAlreadySentException>();
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var person = PersonProvider.Get();
            _personRepository.GetByEmailOrThrowAsync(Arg.Any<Email>())
                .Returns(person);

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<Guid>(), Arg.Any<Guid>())
             .Returns(true);

            _invitationService.IsAnyInvitationFromHouseGroupToPerson(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(false);

            var command = new SendInvitationCommand
            {
                HouseGroupId = InvitationProvider.HouseGroupId,
                PersonId = InvitationProvider.RequestedToPersonId,
                RequestedToPersonEmail = "email@email.com",
            };

            var response = await _commandHandler.Handle(command, default);

            await _invitationRepository.Received(1).AddAsync(Arg.Is<Invitation>(invitation =>
                invitation.Status == InvitationStatus.Pending));

            await _invitationRepository.Received(1).AddAsync(Arg.Is<Invitation>(invitation =>
                invitation.Status == InvitationStatus.Sent));

            response.Data.ShouldNotBeNull();
        }
    }
}
