using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.HouseGroups.Repositories;
using SharedHome.Domain.Shared.ValueObjects;

namespace SharedHome.Application.Bills.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IHouseGroupRepository _houseGroupRepository;

        public BillService(IBillRepository billRepository, IHouseGroupRepository houseGroupRepository)
        {
            _billRepository = billRepository;
            _houseGroupRepository = houseGroupRepository;
        }

        public async Task<Bill> GetAsync(Guid id, Guid personId)
        {
            if (await _houseGroupRepository.IsPersonInHouseGroupAsync(personId))
            {
                var houseGroupPersonIds = await _houseGroupRepository.GetMemberPersonIdsAsync(personId);

                var convertedHouseGroupPersonIds = new List<PersonId>(houseGroupPersonIds.Select(x => new PersonId(x)));

                return await _billRepository.GetOrThrowAsync(id, convertedHouseGroupPersonIds);
            }

            return await _billRepository.GetOrThrowAsync(id, personId);
        }
    }
}
