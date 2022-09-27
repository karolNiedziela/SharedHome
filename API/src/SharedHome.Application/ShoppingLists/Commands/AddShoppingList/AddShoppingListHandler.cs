using MapsterMapper;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Events;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Domain;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingList
{
    public class AddShoppingListHandler : ICommandHandler<AddShoppingListCommand, Response<ShoppingListDto>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        private readonly IDomainEventDispatcher _eventDispatcher;

        public AddShoppingListHandler(IShoppingListRepository shoppingListRepository, IMapper mapper, IDomainEventDispatcher eventDispatcher)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Response<ShoppingListDto>> Handle(AddShoppingListCommand request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<ShoppingListProduct>>(request.Products);

            var shoppingList = ShoppingList.Create(request.Name, request.PersonId!, products: products);

            await _shoppingListRepository.AddAsync(shoppingList);
            await _eventDispatcher.Dispatch(new ShoppingListCreated(shoppingList.Id, shoppingList.Name, new CreatorDto(request.PersonId!, request.FirstName!, request.LastName!)));

            return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList));
        }
    }
}
