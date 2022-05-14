using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.DTO;
using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    internal class GetShoppingListHandler : IQueryHandler<GetShoppingList, Response<ShoppingListDto>>
    {
        private readonly IHouseGroupService _houseGroupService;
        private readonly IMapper _mapper;
        private readonly DbSet<ShoppingListReadModel> _shoppingLists;

        public GetShoppingListHandler(ReadSharedHomeDbContext context, IMapper mapper, 
            IHouseGroupService houseGroupService)
        {
            _mapper = mapper;
            _houseGroupService = houseGroupService;
            _shoppingLists = context.ShoppingLists;
        }

        public async Task<Response<ShoppingListDto>> Handle(GetShoppingList request, CancellationToken cancellationToken)
        {
            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                var houseGroupPersonIds = await _houseGroupService.GetHouseGroupPersonsId(request.PersonId!);

                var shoppingListFromHouseGroup = await _shoppingLists
                    .Include(shoppingList => shoppingList.Person)
                    .Include(shoppingList => shoppingList.Products)
                    .SingleOrDefaultAsync(shoppingList => shoppingList.Id == request.Id && houseGroupPersonIds.Contains(shoppingList.PersonId!));

                return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingListFromHouseGroup));
            }

            var shoppingList = await _shoppingLists
                .Include(shoppingList => shoppingList.Person)
                .Include(shoppingList => shoppingList.Products)
                .SingleOrDefaultAsync(shoppingList => shoppingList.Id == request.Id && shoppingList.PersonId == request.PersonId);

            return new Response<ShoppingListDto>(_mapper.Map<ShoppingListDto>(shoppingList));
        }
    }
}
