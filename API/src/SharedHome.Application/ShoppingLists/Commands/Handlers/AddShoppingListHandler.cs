using MapsterMapper;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists.Aggregates;
using SharedHome.Domain.ShoppingLists.Constants;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Helpers;

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
            var products = request.Products.Select(p => new ShoppingListProduct(p.ProductName,
                                                                                p.Quantity,
                                                                                netContent: new NetContent(p.NetContent,
                                                                                                           p.NetContentType.HasValue ? 
                                                                                                            EnumHelper.ToEnumByIntOrThrow<NetContentType>(p.NetContentType.Value) :
                                                                                                            null)))
                .ToHashSet();
            var shoppingList = ShoppingList.Create(request.Name, request.PersonId!, products: products);

            await _shoppingListRepository.AddAsync(shoppingList);
            //await _messageBroker.PublishAsync(new ShoppingListCreated(shoppingList.Name), cancellationToken);

            return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList));
        }
    }
}
