using MapsterMapper;
using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Events;
using SharedHome.Domain.Common.Events;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Repositories;

using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingList
{
    public class AddShoppingListHandler : IRequestHandler<AddShoppingListCommand, Response<ShoppingListDto>>
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

            var shoppingList = ShoppingList.Create(Guid.NewGuid(), request.Name, request.PersonId, products: products);

            await _shoppingListRepository.AddAsync(shoppingList);
            await _eventDispatcher.Dispatch(new ShoppingListCreated(shoppingList.Id, shoppingList.Name, new CreatorDto(request.PersonId!, request.FirstName!, request.LastName!)));

            return new Response<ShoppingListDto>(_mapper.From<ShoppingListDto>(shoppingList));
        }
    }
}
