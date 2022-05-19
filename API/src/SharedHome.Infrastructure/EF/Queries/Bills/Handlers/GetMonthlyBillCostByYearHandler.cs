using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Application.ReadServices;
using SharedHome.Infrastructure.EF.Contexts;
using SharedHome.Infrastructure.EF.Models;
using SharedHome.Shared.Abstractions.Queries;
using SharedHome.Shared.Abstractions.Responses;
using SharedHome.Shared.Abstractions.Time;
using SharedHome.Shared.Extensionss;
using System.Globalization;

namespace SharedHome.Infrastructure.EF.Queries.Bills.Handlers
{
    internal class GetMonthlyBillCostByYearHandler : IQueryHandler<GetMonthlyBillCostByYear, Response<List<BillMonthlyCostDto>>>
    {
        private readonly DbSet<BillReadModel> _bills;
        private readonly ITime _time;
        private readonly IHouseGroupReadService _houseGroupService;

        public GetMonthlyBillCostByYearHandler(ReadSharedHomeDbContext context, ITime time, IHouseGroupReadService houseGroupService)
        {
            _bills = context.Bills;
            _time = time;
            _houseGroupService = houseGroupService;
        }

        public async Task<Response<List<BillMonthlyCostDto>>> Handle(GetMonthlyBillCostByYear request, CancellationToken cancellationToken)
        {
            if (!request.Year.HasValue)
            {
                var currentDate = _time.CurrentDate();
                request.Year = currentDate.Year;
            }

            var months = Enumerable.Range(1, 12);

            var bills = new List<BillReadModel>();

            var billCostsGroupedByMonth = new List<BillMonthlyCostDto>();

            if (await _houseGroupService.IsPersonInHouseGroup(request.PersonId!))
            {
                var houseGroupPersonsId = await _houseGroupService.GetMemberPersonIds(request.PersonId!);

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
                            TotalCost = billReadModels.Sum(bill => bill.Cost)
                        }
                    )
                    .ToList();

                return new Response<List<BillMonthlyCostDto>>(billCostsGroupedByMonth);
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
                        TotalCost = billReadModels.Sum(bill => bill.Cost)
                    }
                )
                .ToList();

            return new Response<List<BillMonthlyCostDto>>(billCostsGroupedByMonth);
        }
    }
}
