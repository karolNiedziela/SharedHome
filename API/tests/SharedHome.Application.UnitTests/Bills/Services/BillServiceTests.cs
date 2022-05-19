using NSubstitute;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.Bills.Services;
using SharedHome.Application.Services;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Services
{
    public class BillServiceTests
    {
        private readonly IHouseGroupReadService _houseGroupReadService;
        private readonly IBillRepository _billRepository;
        private readonly IBillService _sut;

        public BillServiceTests()
        {
            _houseGroupReadService = Substitute.For<IHouseGroupReadService>();
            _billRepository = Substitute.For<IBillRepository>();
            _sut = new BillService(_billRepository, _houseGroupReadService);
        }

        [Fact]
        public async Task GetAsync_Should_Call_HouseGroupService_Return_Bill_When_Person_Is_In_HouseGroup()
        {
            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(true);

            var personIds = new List<string> { BillProvider.DefaultPersonId, "secondPersonId" };

            _houseGroupReadService.GetMemberPersonIds(Arg.Any<string>())
                .Returns(personIds);

            _billRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<IEnumerable<string>>()).Returns(BillProvider.Get());

            var bill = await _sut.GetAsync(0, BillProvider.DefaultPersonId);

            bill!.PersonId.ShouldBe(BillProvider.DefaultPersonId);
            bill!.BillType.ShouldBe(BillType.Rent);
            await _houseGroupReadService.Received(1).IsPersonInHouseGroup(BillProvider.DefaultPersonId);
            await _billRepository.Received(1).GetAsync(0, personIds);
        }

        [Fact]
        public async Task GetAsync_Should_Call_HouseGroupService_Return_Bill_When_Person_Is_In_Not_HouseGroup()
        {
            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<string>()).Returns(false);

            _billRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()).Returns(BillProvider.Get());

            var bill = await _sut.GetAsync(0, BillProvider.DefaultPersonId);

            bill!.PersonId.ShouldBe(BillProvider.DefaultPersonId);
            bill!.BillType.ShouldBe(BillType.Rent);
            await _houseGroupReadService.Received(1).IsPersonInHouseGroup(BillProvider.DefaultPersonId);
            await _billRepository.Received(1).GetAsync(0, BillProvider.DefaultPersonId);
        }
    }
}
