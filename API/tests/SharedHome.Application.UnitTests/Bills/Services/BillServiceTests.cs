using NSubstitute;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.Bills.Services;
using SharedHome.Domain.Bills.Enums;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.Services;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.HouseGroups.Repositories;
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
        private readonly IHouseGroupRepository _houseGroupRepository;
        private readonly IBillRepository _billRepository;
        private readonly IBillService _sut;

        public BillServiceTests()
        {
            _houseGroupRepository = Substitute.For<IHouseGroupRepository>();
            _billRepository = Substitute.For<IBillRepository>();
            _sut = new BillService(_billRepository, _houseGroupRepository);
        }

        [Fact]
        public async Task GetAsync_Should_Call_HouseGroupService_Return_Bill_When_Person_Is_In_HouseGroup()
        {
            _houseGroupRepository.IsPersonInHouseGroupAsync(Arg.Any<PersonId>()).Returns(true);

            var personIds = new List<Guid> { BillFakeProvider.PersonId };

            var convertedPersonIds = new List<PersonId>(personIds.Select(x => new PersonId(x)));

            _houseGroupRepository.GetMemberPersonIdsAsync(Arg.Any<PersonId>())
                .Returns(personIds);

            _billRepository.GetOrThrowAsync(Arg.Any<BillId>(), Arg.Any<IEnumerable<PersonId>>()).Returns(BillFakeProvider.Get());

            var bill = await _sut.GetAsync(BillFakeProvider.BillId, BillFakeProvider.PersonId);

            bill!.PersonId.Value.ShouldBe(BillFakeProvider.PersonId);
            bill!.BillType.ShouldBe(BillType.Rent);
            await _houseGroupRepository.Received(1).IsPersonInHouseGroupAsync(BillFakeProvider.PersonId);

            await _billRepository.Received(1).GetAsync(BillFakeProvider.BillId, Arg.Any<IEnumerable<PersonId>>());
        }

        [Fact]
        public async Task GetAsync_Should_Call_HouseGroupService_Return_Bill_When_Person_Is_In_Not_HouseGroup()
        {
            _houseGroupRepository.IsPersonInHouseGroupAsync(Arg.Any<PersonId>()).Returns(false);

            _billRepository.GetOrThrowAsync(Arg.Any<BillId>(), Arg.Any<PersonId>()).Returns(BillFakeProvider.Get());

            var bill = await _sut.GetAsync(BillFakeProvider.BillId, BillFakeProvider.PersonId);

            bill!.PersonId.Value.ShouldBe(BillFakeProvider.PersonId);
            bill!.BillType.ShouldBe(BillType.Rent);
            await _houseGroupRepository.Received(1).IsPersonInHouseGroupAsync(BillFakeProvider.PersonId);
            await _billRepository.Received(1).GetAsync(BillFakeProvider.BillId, BillFakeProvider.PersonId);
        }
    }
}
