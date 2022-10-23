using NSubstitute;
using SharedHome.Application.Bills.Exceptions;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.Application.UnitTests.Bills.Extensions
{
    public class BillRepositoryExtensionsTests
    {
        private readonly IBillRepository _billRepository;

        public BillRepositoryExtensionsTests()
        {
            _billRepository = Substitute.For<IBillRepository>();
        }

        [Fact]
        public async Task GetOrThrowAsync_With_PersonId_Parameter_Should_Throw_BillNotFoundException_When_Bill_Not_Found()
        {
            var exception = await Record.ExceptionAsync(() => 
                _billRepository.GetOrThrowAsync(Arg.Any<Guid>(), Arg.Any<Guid>()));

            exception.ShouldBeOfType<BillNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_With_PersonId_Should_Return_Bill_When_Bill_Exists()
        {
            var bill = BillProvider.Get();

            _billRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<Guid>())
                .Returns(bill);

            var returnedBill = await _billRepository.GetOrThrowAsync(BillProvider.BillId, Guid.NewGuid());

            returnedBill.ServiceProvider.Name.ShouldBe(BillProvider.ServiceProviderName);
        }

        [Fact]
        public async Task GetOrThrowAsync_With_PersonIds_Parameter_Should_Throw_BillNotFoundException_Bill_Not_Found()
        {
            var exception = await Record.ExceptionAsync(() =>
                _billRepository.GetOrThrowAsync(Arg.Any<Guid>(), Arg.Any<IEnumerable<Guid>>()));

            exception.ShouldBeOfType<BillNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_With_PersonIds_Should_Return_Bill_When_Bill_Exists()
        {
            var bill = BillProvider.Get();

            _billRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<IEnumerable<Guid>>())
                .Returns(bill);

            var personIds = new List<Guid> { Guid.NewGuid() };
            var returnedBill = await _billRepository.GetOrThrowAsync(BillProvider.BillId, personIds);

            returnedBill.ServiceProvider.Name.ShouldBe(BillProvider.ServiceProviderName);
        }
    }
}
