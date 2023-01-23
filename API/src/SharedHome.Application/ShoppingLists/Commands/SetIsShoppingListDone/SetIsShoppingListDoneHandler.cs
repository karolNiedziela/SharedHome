using MediatR;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.Services;
using SharedHome.Shared.Helpers;

namespace SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone
{
    public class SetIsShoppingListDoneHandler : IRequestHandler<SetIsShoppingListDoneCommand, Unit>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListService _shoppingListService;

        public SetIsShoppingListDoneHandler(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListService = shoppingListService;
        }

        public async Task<Unit> Handle(SetIsShoppingListDoneCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await _shoppingListService.GetAsync(request.ShoppingListId, request.PersonId);

            var status = EnumHelper.ToEnumByIntOrThrow<ShoppingListStatus>(request.Status);

            if (status == ShoppingListStatus.Done)
            {
                shoppingList.MarkAsDone();
            }
            else
            {
                shoppingList.MarkAsToDo();
            }

            await _shoppingListRepository.UpdateAsync(shoppingList);

            return Unit.Value;
        }
    }
}
