using Microsoft.EntityFrameworkCore;
using SharedHome.Application.DTO;
using SharedHome.Application.Services;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Extensionss;
using System.Globalization;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    internal class GetMonthlyShoppingListCostsByYearHandler : IQueryHandler<GetMonthlyShoppingListCostsByYear, Response<List<ShoppingListMonthlyCostDto>>>
    {
        private readonly DbSet<ShoppingListReadModel> _shoppingLists;
        private readonly ITime _time;
        private readonly IHouseGroupService _houseGroupService;

        public GetMonthlyShoppingListCostsByYearHandler(ReadSharedHomeDbContext context, ITime time, IHouseGroupService houseGroupService)
        {
            _shoppingLists = context.ShoppingLists;
            _time = time;
            _houseGroupService = houseGroupService;
        }
        public async Task<Response<List<ShoppingListMonthlyCostDto>>> Handle(GetMonthlyShoppingListCostsByYear request, CancellationToken cancellationToken)
        {
            if (!request.Year.HasValue)
            {
                var currentDate = _time.CurrentDate();
                request.Year = currentDate.Year;
            }

            var months = Enumerable.Range(1, 12);

            var shoppingLists = new List<ShoppingListReadModel>();

            var shoppingListsCostsGroupedByMonth = new List<ShoppingListMonthlyCostDto>();

            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                var houseGroupPersonsId = await _houseGroupService.GetHouseGroupPersonsId(request.PersonId!);

                shoppingLists = await _shoppingLists
                    .Include(shoppingList => shoppingList.Products)
                    .Where(shoppingList => shoppingList.IsDone &&
                        shoppingList.CreatedAt.Year == request.Year &&
                        shoppingList.IsDone &&
                        houseGroupPersonsId.Contains(shoppingList.PersonId))
                    .ToListAsync();

                shoppingListsCostsGroupedByMonth = months
                    .GroupJoin(
                       shoppingLists,
                        month => month,
                        shoppingListReadModel => shoppingListReadModel.CreatedAt.Month,
                        (month, shoppingListReadModels) => new ShoppingListMonthlyCostDto
                        {
                            MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).FirstCharToUpper(),
                            TotalCost = SumProductPrices(shoppingListReadModels)
                        }
                    )
                    .ToList();

                return new Response<List<ShoppingListMonthlyCostDto>>(shoppingListsCostsGroupedByMonth);
            }

            shoppingLists = await _shoppingLists
                .Include(shoppingList => shoppingList.Products)
                .Where(shoppingList => shoppingList.IsDone &&
                    shoppingList.CreatedAt.Year == request.Year &&
                    shoppingList.IsDone &&
                     shoppingList.PersonId == request.PersonId!)
                .ToListAsync();

            shoppingListsCostsGroupedByMonth = months
                   .GroupJoin(
                      shoppingLists,
                       month => month,
                       shoppingListReadModel => shoppingListReadModel.CreatedAt.Month,
                       (month, shoppingListReadModels) => new ShoppingListMonthlyCostDto
                       {
                           MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).FirstCharToUpper(),
                           TotalCost = SumProductPrices(shoppingListReadModels)
                       }
                   )
                   .ToList();

            return new Response<List<ShoppingListMonthlyCostDto>>(shoppingListsCostsGroupedByMonth);
        }

        private decimal SumProductPrices(IEnumerable<ShoppingListReadModel> shoppingLists)
             => shoppingLists.SelectMany(shoppingList => shoppingList.Products).Where(product => product.IsBought).Aggregate((decimal)0, (count, product) => count + (product.Quantity * (decimal)product.Price!));
    }
}
