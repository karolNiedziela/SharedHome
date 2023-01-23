using MapsterMapper;
using MediatR;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Domain.ShoppingLists;
using SharedHome.Domain.ShoppingLists.Entities;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Domain.ShoppingLists.Repositories;
using SharedHome.Domain.ShoppingLists.ValueObjects;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Helpers;
using SharedHome.Shared.Time;

namespace SharedHome.Application.ShoppingLists.Commands.AddShoppingList
{
    public class AddShoppingListHandler : IRequestHandler<AddShoppingListCommand, ApiResponse<ShoppingListDto>>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        private readonly ITimeProvider _timeProvider;

        public AddShoppingListHandler(IShoppingListRepository shoppingListRepository, IMapper mapper, ITimeProvider timeProvider)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
            _timeProvider = timeProvider;
        }

        public async Task<ApiResponse<ShoppingListDto>> Handle(AddShoppingListCommand request, CancellationToken cancellationToken)
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

            var shoppingList = ShoppingList.Create(Guid.NewGuid(), request.Name, request.PersonId, _timeProvider.CurrentDate(), products: products);

            await _shoppingListRepository.AddAsync(shoppingList); 

            return new ApiResponse<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList));
        }
    }
}
