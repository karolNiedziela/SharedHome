using MapsterMapper;
using MediatR;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts
{
    public class AddShoppingListProductsHandler : ICommandHandler<AddShoppingListProductsCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;
        private readonly IMapper _mapper;

        public AddShoppingListProductsHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService, IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddShoppingListProductsCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            var products = _mapper.Map<IEnumerable<ShoppingListProduct>>(request.Products);

            shoppingList.AddProducts(products);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
