using SharedHome.Application.Bills.Exceptions;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Application.Bills.Extensions
{
    public static class BillRepositoryExtensions
    {
        public static async Task<Bill> GetOrThrowAsync(this IBillRepository billRepository, 
            Guid id, Guid personId)
        {
            var bill = await billRepository.GetAsync(id, personId);
            if (bill is null)
            {
                throw new BillNotFoundException(id);
            }

            return bill;
        }

        public static async Task<Bill> GetOrThrowAsync(this IBillRepository billRepository,
            Guid id, IEnumerable<Guid> personIds)
        {
            var personIdsConverted = new List<PersonId>();
            foreach (var personId in personIds)
            {
                personIdsConverted.Add(personId);
            }

            var bill = await billRepository.GetAsync(id, personIdsConverted);
            if (bill is null)
            {
                throw new BillNotFoundException(id);
            }

            return bill;
        }
    }
}
