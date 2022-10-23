using MediatR;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.UpdateShoppingList
{
    public class UpdateShoppingListHandler : ICommandHandler<UpdateShoppingListCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public UpdateShoppingListHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            shoppingList.Update(request.Name);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
