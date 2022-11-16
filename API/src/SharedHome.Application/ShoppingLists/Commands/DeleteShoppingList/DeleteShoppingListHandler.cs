using MediatR;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;


namespace SharedHome.Application.ShoppingLists.Commands.DeleteShoppingList
{
    public class DeleteShoppingListHandler : IRequestHandler<DeleteShoppingListCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public DeleteShoppingListHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            await _shoppingListRepository.DeleteAsync(shoppingList);

            return Unit.Value;
        }
    }
}
