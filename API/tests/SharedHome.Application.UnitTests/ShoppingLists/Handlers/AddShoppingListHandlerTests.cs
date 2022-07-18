using AutoMapper;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Commands.Handlers;
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
        private readonly ICommandHandler<AddShoppingList, Response<ShoppingListDto>> _commandHandler;

        public AddShoppingListHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            var mapperConfiguration = new MapperConfiguration(config => config.AddMaps(Assembly.GetAssembly(typeof(InfrastructureAssemblyReference))));
            _mapper = new Mapper(mapperConfiguration);
            _commandHandler = new AddShoppingListHandler(_shoppingListRepository, _mapper);
        }


        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddShoppingList
            {
                Name = "ShoppingList"
            };

            var response = await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).AddAsync(Arg.Any<ShoppingList>());

            response.Data.ShouldNotBeNull();
        }
    }
}
