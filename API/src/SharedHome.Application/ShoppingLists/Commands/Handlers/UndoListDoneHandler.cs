using MediatR;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class UndoListDoneHandler : ICommandHandler<UndoListDone, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public UndoListDoneHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<Unit> Handle(UndoListDone request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListRepository.GetOrThrowAsync(request.ShoppingListId);

            shoppingList.UndoListDone();

            return Unit.Value;
        }
    }
}
