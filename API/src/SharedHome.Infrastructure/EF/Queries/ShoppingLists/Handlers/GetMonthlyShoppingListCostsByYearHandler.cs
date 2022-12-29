using Microsoft.EntityFrameworkCore;
using SharedHome.Application.ReadServices;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using MediatR;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Time;
using SharedHome.Shared.Extensionss;
using System.Globalization;

namespace SharedHome.Infrastructure.EF.Queries.ShoppingLists.Handlers
{
    internal class GetMonthlyShoppingListCostsByYearHandler : IRequestHandler<GetMonthlyShoppingListCostsByYear, Response<List<ShoppingListMonthlyCostDto>>>
    {
        private readonly DbSet<ShoppingListReadModel> _shoppingLists;
        private readonly ITimeProvider _time;
        private readonly IHouseGroupReadService _houseGroupService;

        public GetMonthlyShoppingListCostsByYearHandler(ReadSharedHomeDbContext context, ITimeProvider time, IHouseGroupReadService houseGroupService)
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

            if (await _houseGroupService.IsPersonInHouseGroupAsync(request.PersonId!))
            {
                var houseGroupPersonsId = await _houseGroupService.GetMemberPersonIdsAsync(request.PersonId!);

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
                            TotalCost = SumProductPrices(shoppingListReadModels),
                            Currency = GetCurrency(shoppingListReadModels)
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
                           TotalCost = SumProductPrices(shoppingListReadModels),
                           Currency = GetCurrency(shoppingListReadModels)
                       }
                   )
                   .ToList();

            return new Response<List<ShoppingListMonthlyCostDto>>(shoppingListsCostsGroupedByMonth);
        }

        private static decimal SumProductPrices(IEnumerable<ShoppingListReadModel> shoppingLists)
             => shoppingLists
            .Where(shoppingList => shoppingList.IsDone)
            .SelectMany(shoppingList => shoppingList.Products)
            .Where(product => product.IsBought)
            .Aggregate((decimal)0, (count, product) => count + (product.Quantity * (decimal)product.Price!));

        private static string GetCurrency(IEnumerable<ShoppingListReadModel> shoppingLists)
            => shoppingLists.Where(x => x.IsDone)
            .SelectMany(shoppingList => shoppingList.Products)
            .Where(product => product.IsBought)
            .FirstOrDefault()?.Currency ?? string.Empty;
  
    }
}
