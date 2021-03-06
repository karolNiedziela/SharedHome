using AutoMapper;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.ShoppingLists.Commands.Handlers
{
    public class AddShoppingListHandler : ICommandHandler<AddShoppingList, Response<ShoppingListDto>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        //private readonly IMessageBroker _messageBroker;

        public AddShoppingListHandler(IShoppingListRepository shoppingListRepository, IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
            //_messageBroker = messageBroker;
        }

        public async Task<Response<ShoppingListDto>> Handle(AddShoppingList request, CancellationToken cancellationToken)
        {
            var shoppingList = ShoppingList.Create(request.Name, request.PersonId!);

            await _shoppingListRepository.AddAsync(shoppingList);
            //await _messageBroker.PublishAsync(new ShoppingListCreated(shoppingList.Name), cancellationToken);

            return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList));
        }
    }
}
