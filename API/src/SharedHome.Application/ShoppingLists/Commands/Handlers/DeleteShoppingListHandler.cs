using MediatR;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class DeleteShoppingListHandler : ICommandHandler<DeleteShoppingList, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public DeleteShoppingListHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Unit> Handle(DeleteShoppingList request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepository.GetOrThrowAsync(request.ShoppingListId, request.PersonId!);

            await _shoppingListRepository.DeleteAsync(shoppingList);

            return Unit.Value;
        }
    }
}
