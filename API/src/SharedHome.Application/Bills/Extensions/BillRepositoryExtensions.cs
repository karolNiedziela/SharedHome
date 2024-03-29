﻿using SharedHome.Application.Bills.Exceptions;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Application.Bills.Extensions
{
    public static class BillRepositoryExtensions
    {
        public static async Task<Bill> GetOrThrowAsync(this IBillRepository billRepository, 
            BillId id, PersonId personId)
        {
            var bill = await billRepository.GetAsync(id, personId);
            if (bill is null)
            {
                throw new BillNotFoundException(id);
            }

            return bill;
        }

        public static async Task<Bill> GetOrThrowAsync(this IBillRepository billRepository,
            BillId id, IEnumerable<PersonId> personIds)
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
