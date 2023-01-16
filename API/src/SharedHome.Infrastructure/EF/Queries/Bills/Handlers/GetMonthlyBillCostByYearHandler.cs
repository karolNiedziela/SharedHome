using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Application.ReadServices;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using MediatR;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Time;
using SharedHome.Shared.Extensionss;
using System.Globalization;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    internal class GetMonthlyBillCostByYearHandler : IRequestHandler<GetMonthlyBillCostByYear, ApiResponse<List<BillMonthlyCostDto>>>
    {
        private readonly DbSet<BillReadModel> _bills;
        private readonly ITimeProvider _time;
        private readonly IHouseGroupReadService _houseGroupService;

        public GetMonthlyBillCostByYearHandler(ReadSharedHomeDbContext context, ITimeProvider time, IHouseGroupReadService houseGroupService)
        {
            _bills = context.Bills;
            _time = time;
            _houseGroupService = houseGroupService;
        }

        public async Task<ApiResponse<List<BillMonthlyCostDto>>> Handle(GetMonthlyBillCostByYear request, CancellationToken cancellationToken)
        {
            if (!request.Year.HasValue)
            {
                var currentDate = _time.CurrentDate();
                request.Year = currentDate.Year;
            }

            var months = Enumerable.Range(1, 12);

            var bills = new List<BillReadModel>();

            var billCostsGroupedByMonth = new List<BillMonthlyCostDto>();

            if (await _houseGroupService.IsPersonInHouseGroupAsync(request.PersonId!))
            {
                var houseGroupPersonsId = await _houseGroupService.GetMemberPersonIdsAsync(request.PersonId!);

                bills = await _bills.Where(bill => bill.IsPaid &&
                    bill.DateOfPayment.Year == request.Year &&
                     houseGroupPersonsId.Contains(bill.PersonId))
                    .ToListAsync();

                billCostsGroupedByMonth = months
                    .GroupJoin(
                       bills,
                        month => month,
                        billReadModel => billReadModel.DateOfPayment.Month,
                        (month, billReadModels) => new BillMonthlyCostDto
                        {
                            MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).FirstCharToUpper(),
                            TotalCost = SumCost(billReadModels),
                            Currency = GetCurrency(billReadModels)
                        }
                    )
                    .ToList();

                return new ApiResponse<List<BillMonthlyCostDto>>(billCostsGroupedByMonth);
            }

            bills = await _bills.Where(bill => bill.IsPaid && 
                bill.DateOfPayment.Year == request.Year && 
                bill.PersonId == request.PersonId!)
                .ToListAsync();

            billCostsGroupedByMonth = months
                .GroupJoin(
                    bills,
                    month => month,
                    billReadModel => billReadModel.DateOfPayment.Month,
                    (month, billReadModels) => new BillMonthlyCostDto
                    {
                        MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).FirstCharToUpper(),
                        TotalCost = SumCost(billReadModels),
                        Currency = GetCurrency(billReadModels)
                    }
                )
                .ToList();

            return new ApiResponse<List<BillMonthlyCostDto>>(billCostsGroupedByMonth);
        }

        private static decimal? SumCost(IEnumerable<BillReadModel> bills)
        => bills.Sum(bill => bill.Cost);

        private static string GetCurrency(IEnumerable<BillReadModel> bills)
          => bills.Where(x => x.IsPaid)
          .FirstOrDefault()?.Currency ?? string.Empty;
    }
}
