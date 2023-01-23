using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Common.Queries;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Domain.ShoppingLists.Enums;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Extensions;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Time;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    internal class GetAllShoppingListsByYearAndMonthAndIsDoneHandler : IRequestHandler<GetAllShoppingListsByYearAndMonthAndIsDone, Paged<ShoppingListDto>>
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

            if (request.Status != (int)ShoppingListStatus.ToDo && request.Status != (int)ShoppingListStatus.Done)
            {
                request.Status = (int)ShoppingListStatus.ToDo;
            }

            if (await _houseGroupService.IsPersonInHouseGroupAsync(request.PersonId!))
            {
                var houseGroupPersonIds = await _houseGroupService.GetMemberPersonIdsAsync(request.PersonId!);

                return await _shoppingLists
                    .Include(shoppingList => shoppingList.Person)
                    .Include(shoppingLists => shoppingLists.Products)
                    .Where(shoppingList => shoppingList.CreationDate.Month == request.Month.Value &&
                    shoppingList.CreationDate.Year == request.Year.Value &&
                    shoppingList.Status == request.Status &&
                    houseGroupPersonIds.Contains(shoppingList.PersonId))
                    .OrderByDescending(shoppingList => shoppingList.CreationDate)
                    .Select(shoppingList => _mapper.Map<ShoppingListDto>(shoppingList))
                    .PaginateAsync(request.PageNumber, request.PageSize);
            }

            return await _shoppingLists
                .Include(shoppingList => shoppingList.Person)
                .Include(shoppingList => shoppingList.Products)
                .Where(shoppingList => shoppingList.CreationDate.Month == request.Month.Value &&
                shoppingList.CreationDate.Year == request.Year.Value &&
                shoppingList.PersonId == request.PersonId &&
                shoppingList.Status == request.Status)
                .OrderByDescending(shoppingList => shoppingList.CreationDate)
                .Select(shoppingList => _mapper.Map<ShoppingListDto>(shoppingList))
                .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
