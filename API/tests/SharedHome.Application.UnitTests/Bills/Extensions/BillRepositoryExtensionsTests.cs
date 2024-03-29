﻿using NSubstitute;
using SharedHome.Application.Bills.Exceptions;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Domain.Bills.Repositories;
using SharedHome.Domain.Bills.ValueObjects;
using SharedHome.Domain.Shared.ValueObjects;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _billRepository.GetOrThrowAsync(new BillId(), Arg.Any<PersonId>()));

            exception.ShouldBeOfType<BillNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_With_PersonId_Should_Return_Bill_When_Bill_Exists()
        {
            var bill = BillFakeProvider.Get();

            _billRepository.GetAsync(Arg.Any<BillId>(), Arg.Any<PersonId>())
                .Returns(bill);

            var returnedBill = await _billRepository.GetOrThrowAsync(BillFakeProvider.BillId, Guid.NewGuid());

            returnedBill.ServiceProvider.Name.ShouldBe(BillFakeProvider.ServiceProviderName);
        }

        [Fact]
        public async Task GetOrThrowAsync_With_PersonIds_Parameter_Should_Throw_BillNotFoundException_Bill_Not_Found()
        {
            var exception = await Record.ExceptionAsync(() =>
                _billRepository.GetOrThrowAsync(new BillId(), Enumerable.Empty<PersonId>()));

            exception.ShouldBeOfType<BillNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_With_PersonIds_Should_Return_Bill_When_Bill_Exists()
        {
            var bill = BillFakeProvider.Get();
            var personIds = new List<PersonId> { Guid.NewGuid() };

            _billRepository.GetAsync(BillFakeProvider.BillId, personIds)
                .Returns(bill);

            var returnedBill = await _billRepository.GetOrThrowAsync(BillFakeProvider.BillId, personIds);

            returnedBill.ServiceProvider.Name.ShouldBe(BillFakeProvider.ServiceProviderName);
        }
    }
}
