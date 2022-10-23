using MediatR;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.DeleteManyShoppingListProducts
{
    public class DeleteManyShoppingListProductsHandler : ICommandHandler<DeleteManyShoppingListProductsCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public DeleteManyShoppingListProductsHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(DeleteManyShoppingListProductsCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            shoppingList.RemoveProducts(request.ProductNames);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
