﻿using Mapster;
using MapsterMapper;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingList;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Infrastructure;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using Shouldly;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class AddShoppingListHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        private readonly ICommandHandler<AddShoppingListCommand, Response<ShoppingListDto>> _commandHandler;

        public AddShoppingListHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetAssembly(typeof(InfrastructureAssemblyReference))!);
            _mapper = new Mapper(config);
            _commandHandler = new AddShoppingListHandler(_shoppingListRepository, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddShoppingListCommand
            {
                Name = "ShoppingList"
            };

            var response = await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).AddAsync(Arg.Any<ShoppingList>());

            response.Data.ShouldNotBeNull();
        }
    }
}
