using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using MediatR;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    internal class GetShoppingListHandler : IRequestHandler<GetShoppingList, Response<ShoppingListDto>>
    {
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly IMapper _mapper;
        private readonly DbSet<ShoppingListReadModel> _shoppingLists;

        public GetShoppingListHandler(ReadSharedHomeDbContext context, IMapper mapper, 
            IHouseGroupReadService houseGroupService)
        {
            _mapper = mapper;
            _houseGroupService = houseGroupService;
            _shoppingLists = context.ShoppingLists;
        }

        public async Task<Response<ShoppingListDto>> Handle(GetShoppingList request, CancellationToken cancellationToken)
        {
            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                var houseGroupPersonIds = await _houseGroupService.GetMemberPersonIds(request.PersonId!);

                var shoppingListFromHouseGroup = await _shoppingLists
                    .Include(shoppingList => shoppingList.Person)
                    .Include(shoppingList => shoppingList.Products.OrderBy(x => x.IsBought))
                    .SingleOrDefaultAsync(shoppingList => shoppingList.Id == request.Id && houseGroupPersonIds.Contains(shoppingList.PersonId!));

                return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingListFromHouseGroup!));
            }

            var shoppingList = await _shoppingLists
                .Include(shoppingList => shoppingList.Person)
                .Include(shoppingList => shoppingList.Products.OrderBy(x => x.IsBought))
                .SingleOrDefaultAsync(shoppingList => shoppingList.Id == request.Id && shoppingList.PersonId == request.PersonId);

            return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList!));
        }
    }
}
