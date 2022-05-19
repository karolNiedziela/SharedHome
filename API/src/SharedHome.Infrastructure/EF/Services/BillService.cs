using Microsoft.EntityFrameworkCore;
using SharedHome.Application.Bills.Exceptions;
using SharedHome.Application.Services;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Infrastructure.EF.Contexts;

namespace SharedHome.Infrastructure.EF.Services
{
    public class BillService : IBillService
    {
        private readonly WriteSharedHomeDbContext _context;
        private readonly IHouseGroupReadService _houseGroupService;

        public BillService(WriteSharedHomeDbContext context, IHouseGroupReadService houseGroupService)
        {
            _context = context;
            _houseGroupService = houseGroupService;
        }

        public async Task<Bill> GetForHouseGroupMemberAsync(int billId, string personId)
        {
            var houseGroupPersonIds = await _houseGroupService.GetMemberPersonIds(personId);

            var bill = await _context.Bills
                .SingleOrDefaultAsync(bill => bill.Id == billId &&
                houseGroupPersonIds.Contains(bill.PersonId));

            if (bill is null)
            {
                throw new BillNotFoundException(billId);
            }

            return bill;
        }
    }
}
