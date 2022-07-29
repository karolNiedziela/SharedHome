using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Extensions;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Time;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    internal class GetAllShoppingListsByYearAndMonthAndIsDoneHandler : IQueryHandler<GetAllShoppingListsByYearAndMonthAndIsDone, Paged<ShoppingListDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITimeProvider _time;
        private readonly IHouseGroupReadService _houseGroupService;
        private readonly DbSet<ShoppingListReadModel> _shoppingLists;

        public GetAllShoppingListsByYearAndMonthAndIsDoneHandler(IMapper mapper, ITimeProvider time,
            IHouseGroupReadService houseGroupService, ReadSharedHomeDbContext context)
        {
            _mapper = mapper;
            _time = time;
            _houseGroupService = houseGroupService;
            _shoppingLists = context.ShoppingLists;
        }

        public async Task<Paged<ShoppingListDto>> Handle(GetAllShoppingListsByYearAndMonthAndIsDone request, CancellationToken cancellationToken)
        {
            if (!request.Year.HasValue || !request.Month.HasValue)
            {
                var currentDate = _time.CurrentDate();
                request.Year = currentDate.Year;
                request.Month = currentDate.Month;
            }

            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                var houseGroupPersonIds = await _houseGroupService.GetMemberPersonIds(request.PersonId!);

                return await _shoppingLists
                    .Include(shoppingList => shoppingList.Person)
                    .Include(shoppingLists => shoppingLists.Products)
                    .Where(shoppingList => shoppingList.CreatedAt.Month == request.Month.Value &&
                    shoppingList.CreatedAt.Year == request.Year.Value &&
                    shoppingList.IsDone == request!.IsDone &&
                    houseGroupPersonIds.Contains(shoppingList.PersonId))
                    .Select(shoppingList => _mapper.Map<ShoppingListDto>(shoppingList))
                    .PaginateAsync(request.PageNumber, request.PageSize);
            }

            return await _shoppingLists
                .Include(shoppingList => shoppingList.Person)
                .Include(shoppingList => shoppingList.Products)
                .Where(shoppingList => shoppingList.CreatedAt.Month == request.Month.Value &&
                shoppingList.CreatedAt.Year == request.Year.Value &&
                shoppingList.PersonId == request.PersonId &&
                shoppingList.IsDone == request!.IsDone)
                .Select(shoppingList => _mapper.Map<ShoppingListDto>(shoppingList))
                .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
