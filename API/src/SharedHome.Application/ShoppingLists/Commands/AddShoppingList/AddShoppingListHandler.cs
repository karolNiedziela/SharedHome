using MapsterMapper;
using MediatR;
using SharedHome.Application.Common.DTO;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Events;
using SharedHome.Domain.Common.Events;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Helpers;

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
            var products = new List<ShoppingListProduct>();

            foreach (var product in request.Products)
            {
                NetContent? netContent;

                if (product.NetContent == null || product.NetContent.NetContent == null)
                {
                    netContent = null;
                }
                else if (!product.NetContent.NetContentType.HasValue)
                {
                    netContent = null;
                }
                else
                {
                    netContent = new NetContent(product.NetContent.NetContent, EnumHelper.ToEnumByIntOrThrow<NetContentType>(product.NetContent.NetContentType.Value));
                }

                products.Add(ShoppingListProduct.Create(product.Name, product.Quantity, null, netContent, false, Guid.NewGuid()));
            }

            var shoppingList = ShoppingList.Create(Guid.NewGuid(), request.Name, request.PersonId, products: products);

            await _shoppingListRepository.AddAsync(shoppingList);
            await _eventDispatcher.Dispatch(new ShoppingListCreated(shoppingList.Id, shoppingList.Name, new CreatorDto(request.PersonId!, request.FirstName!, request.LastName!)));

            return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList));
        }
    }
}
