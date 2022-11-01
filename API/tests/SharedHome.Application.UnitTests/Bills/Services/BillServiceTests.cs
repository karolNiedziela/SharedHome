using NSubstitute;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.Bills.Services;
using SharedHome.Application.ReadServices;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<Guid>()).Returns(true);

            var personIds = new List<Guid> { BillProvider.PersonId };

            var convertedPersonIds = new List<PersonId>(personIds.Select(x => new PersonId(x)));

            _houseGroupReadService.GetMemberPersonIds(Arg.Any<Guid>())
                .Returns(personIds);

            _billRepository.GetOrThrowAsync(Arg.Any<BillId>(), Arg.Any<IEnumerable<PersonId>>()).Returns(BillProvider.Get());

            var bill = await _sut.GetAsync(BillProvider.BillId, BillProvider.PersonId);

            bill!.PersonId.Value.ShouldBe(BillProvider.PersonId);
            bill!.BillType.ShouldBe(BillType.Rent);
            await _houseGroupReadService.Received(1).IsPersonInHouseGroup(BillProvider.PersonId);

            await _billRepository.Received(1).GetAsync(BillProvider.BillId, Arg.Any<IEnumerable<PersonId>>());
        }

        [Fact]
        public async Task GetAsync_Should_Call_HouseGroupService_Return_Bill_When_Person_Is_In_Not_HouseGroup()
        {
            _houseGroupReadService.IsPersonInHouseGroup(Arg.Any<Guid>()).Returns(false);

            _billRepository.GetOrThrowAsync(Arg.Any<BillId>(), Arg.Any<PersonId>()).Returns(BillProvider.Get());

            var bill = await _sut.GetAsync(BillProvider.BillId, BillProvider.PersonId);

            bill!.PersonId.Value.ShouldBe(BillProvider.PersonId);
            bill!.BillType.ShouldBe(BillType.Rent);
            await _houseGroupReadService.Received(1).IsPersonInHouseGroup(BillProvider.PersonId);
            await _billRepository.Received(1).GetAsync(BillProvider.BillId, BillProvider.PersonId);
        }
    }
}
