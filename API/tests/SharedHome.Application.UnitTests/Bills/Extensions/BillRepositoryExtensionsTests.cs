using NSubstitute;
using SharedHome.Application.Bills.Exceptions;
using SharedHome.Application.Bills.Extensions;
using SharedHome.Application.UnitTests.Providers;
using SharedHome.Domain.Bills.Repositories;
using Shouldly;
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
        public async Task GetOrThrowAsync_Should_Throw_BillNotFoundException_When_Bill_Not_Found()
        {
            var exception = await Record.ExceptionAsync(() => 
                _billRepository.GetOrThrowAsync(Arg.Any<int>(), Arg.Any<string>()));

            exception.ShouldBeOfType<BillNotFoundException>();
        }

        [Fact]
        public async Task GetOrThrowAsync_Should_Return_Bill_When_Bill_Exists()
        {
            var bill = BillProvider.Get();

            _billRepository.GetAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(bill);

            var returnedBill = await _billRepository.GetOrThrowAsync(1, "personId");

            returnedBill.ServiceProvider.Name.ShouldBe(BillProvider.DefaultServiceProviderName);
        }
    }
}
