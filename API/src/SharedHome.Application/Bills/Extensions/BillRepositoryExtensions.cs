using SharedHome.Application.Bills.Exceptions;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;

namespace SharedHome.Application.Bills.Extensions
{
    public static class BillRepositoryExtensions
    {
        public static async Task<Bill> GetOrThrowAsync(this IBillRepository billRepository, int id, string personId)
        {
            var bill = await billRepository.GetAsync(id, personId);
            if (bill is null)
            {
                throw new BillNotFoundException(id);
            }

            return bill;
        }

        public static async Task<Bill> GetOrThrowAsync(this IBillRepository billRepository, int id, IEnumerable<string> personIds)
        {
            var bill = await billRepository.GetAsync(id, personIds);
            if (bill is null)
            {
                throw new BillNotFoundException(id);
            }

            return bill;
        }
    }
}
