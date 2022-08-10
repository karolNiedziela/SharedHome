using Mapster;
using MapsterMapper;
using MediatR;
using NSubstitute;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Infrastructure;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Tests.Shared.Providers;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.ShoppingLists.Handlers
{
    public class AddShoppingListProductHandlerTests
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly IMapper _mapper;
        private readonly ICommandHandler<AddShoppingListProductsCommand, Unit> _commandHandler;

        public AddShoppingListProductHandlerTests()
        {
            _shoppingListRepository = Substitute.For<IShoppingListRepository>();
            _shoppingListService = Substitute.For<IShoppingListService>();
            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetAssembly(typeof(InfrastructureAssemblyReference))!);
            _mapper = new Mapper(config);
            _commandHandler = new AddShoppingListProductsHandler(_shoppingListRepository, _shoppingListService, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Call_Repository_OnSuccess()
        {
            var command = new AddShoppingListProductsCommand
            {
                ShoppingListId = 1,
                Products = new List<AddShoppingListProductDto>
                {
                    new AddShoppingListProductDto
                    {
                         Name = "Product",
                         Quantity = 1,
                         NetContent = "100",
                         NetContentType = 1
                    }
                }
               
            };

            _shoppingListService.GetAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(ShoppingListProvider.GetEmpty());

            await _commandHandler.Handle(command, default);

            await _shoppingListRepository.Received(1).UpdateAsync(Arg.Any<ShoppingList>());
        }
    }
}
