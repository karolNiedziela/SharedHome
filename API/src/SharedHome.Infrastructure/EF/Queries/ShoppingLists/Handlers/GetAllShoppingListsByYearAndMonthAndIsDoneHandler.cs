using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.DTO;
using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Time;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    public class GetAllShoppingListsByYearAndMonthAndIsDoneHandler : IQueryHandler<GetAllShoppingListsByYearAndMonthAndIsDone, Paged<ShoppingListDto>>
    {
        private readonly SharedHomeDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITime _time;
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IHouseGroupService _houseGroupService;

        public GetAllShoppingListsByYearAndMonthAndIsDoneHandler(SharedHomeDbContext dbContext, IMapper mapper, ITime time,
            IHouseGroupRepository houseGroupRepository, IHouseGroupService houseGroupService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _time = time;
            _houseGroupRepository = houseGroupRepository;
            _houseGroupService = houseGroupService;
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
                var houseGroupPersonIds = await _dbContext.HouseGroups
                    .Include(houseGroup => houseGroup.Members)
                    .SelectMany(houseGroup => houseGroup.Members
                    .Select(member => member.PersonId))
                    .ToListAsync();

                return await _dbContext.ShoppingLists
                    .Include(shoppingLists => shoppingLists.Products)
                    .Where(shoppingList => shoppingList.CreatedAt.Month == request.Month.Value &&
                    shoppingList.CreatedAt.Year == request.Year.Value &&
                    shoppingList.IsDone == request!.IsDone &&
                    houseGroupPersonIds.Contains(shoppingList.PersonId))
                    .Select(shoppingList => _mapper.Map<ShoppingListDto>(shoppingList))
                    .PaginateAsync(request.PageNumber, request.PageSize);
            }

            return await _dbContext.ShoppingLists
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
