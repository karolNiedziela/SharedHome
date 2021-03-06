using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Bills.Entities;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;

namespace SharedHome.Application.Bills.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IHouseGroupReadService _houseGroupReadService;

        public BillService(IBillRepository billRepository, IHouseGroupReadService houseGroupReadService)
        {
            _billRepository = billRepository;
            _houseGroupReadService = houseGroupReadService;
        }

        public async Task<Bill> GetAsync(int id, string personId)
        {
            if (await _houseGroupReadService.IsPersonInHouseGroup(personId))
            {
                var houseGroupPersonIds = await _houseGroupReadService.GetMemberPersonIds(personId);

                return await _billRepository.GetOrThrowAsync(id, houseGroupPersonIds);
            }

            return await _billRepository.GetOrThrowAsync(id, personId);
        }
    }
}
