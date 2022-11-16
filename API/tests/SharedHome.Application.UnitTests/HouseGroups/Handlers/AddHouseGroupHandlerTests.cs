﻿using Mapster;
using MapsterMapper;
using MediatR;
using NSubstitute;
using SharedHome.Application.HouseGroups.Commands.AddHouseGroup;
using SharedHome.Application.HouseGroups.DTO;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.HouseGroups;
using SharedHome.Domain.HouseGroups.Exceptions;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Infrastructure;
using SharedHome.Shared.Application.Responses;
using Shouldly;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.HouseGroups.Handlers
{
    public class AddHouseGroupHandlerTests
    {
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IMapper _mapper;
        private readonly IRequestHandler<AddHouseGroupCommand, Response<HouseGroupDto>> _commandHandler;
        
        public AddHouseGroupHandlerTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _houseGroupService = Substitute.For<IHouseGroupReadService>();
            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetAssembly(typeof(InfrastructureAssemblyReference))!);
            _mapper = new Mapper(config);
            _commandHandler = new AddHouseGroupHandler(_houseGroupRepository, _houseGroupService, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Throw_PersonIsAlreadyInHouseGroupException_When_Person_Is_Already_In_HouseGroup()
        {
            var command = new AddHouseGroupCommand
            {
                PersonId = Guid.NewGuid(),
                Name = "HouseGroupName"
            };

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<Guid>()).Returns(true);

            var exception = await Record.ExceptionAsync(() => _commandHandler.Handle(command, default));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PersonIsAlreadyInHouseGroupException>();
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddHouseGroupCommand
            {
                PersonId = Guid.NewGuid(),
                Name = "HouseGroupName"
            };

            _houseGroupService.IsPersonInHouseGroup(Arg.Any<Guid>()).Returns(false);

            var response = await _commandHandler.Handle(command, default);

            await _houseGroupRepository.Received(1).AddAsync(Arg.Is<HouseGroup>(houseGroup =>
            houseGroup.Members.First().PersonId.Value == command.PersonId));

            response.Data.ShouldNotBeNull();
        }
    }
}
