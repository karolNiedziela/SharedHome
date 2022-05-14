using MediatR;
using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Extensions;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class DeleteShoppingListProductHandler : ICommandHandler<DeleteShoppingListProduct, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IHouseGroupService _houseGroupService;

        public DeleteShoppingListProductHandler(IShoppingListRepository shoppingListRepository, IHouseGroupService houseGroupService)
        {
            _shoppingListRepository = shoppingListRepository;
            _houseGroupService = houseGroupService;
        }

        public async Task<Unit> Handle(DeleteShoppingListProduct request, CancellationToken cancellationToken)
        {
            var shoppingList = await _houseGroupService.IsPersonInHouseGroup(request.PersonId!) ?
                 await _houseGroupService.GetShoppingListAsync(request.ShoppingListId, request.PersonId!) :
                 await _shoppingListRepository.GetOrThrowAsync(request.ShoppingListId, request.PersonId!);

            shoppingList.RemoveProduct(request.ProductName);

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
