using MediatR;
using SharedHome.Application.ShoppingLists.Exceptions;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class DeleteShoppingListProductHandler : ICommandHandler<DeleteShoppingListProduct, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public DeleteShoppingListProductHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Unit> Handle(DeleteShoppingListProduct request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepository.GetOrThrowAsync(request.ShoppingListId, request.PersonId!);

            shoppingList.RemoveProduct(request.ProductName);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
