using SharedHome.Api.Constants;
using SharedHome.Application.Bills.Commands;
using SharedHome.Domain.Bills.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.IntegrationTests.Fixtures;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers
{
    public class BillsControllerTests : ControllerTests, IClassFixture<DatabaseFixture>, IDisposable
    {
        private const string BaseAddress = "api/bills";

        public DatabaseFixture Fixture { get; set; }

        public BillsControllerTests(SettingsProvider settingsProvider, DatabaseFixture fixture) : base(settingsProvider)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task Get_Should_Return_200_Status_Code()
        {
            await AddBill();

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.Bills.Get.Replace("{billId:int}", "1")}";

            var response = await Client.GetAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }



        private async Task<Bill> AddBill()
        {
            var bill = BillProvider.Get();

            Authorize(userId: bill.PersonId);

            var endpointAddress = $"{BaseAddress}";
            var command = new AddBill
            {
                BillType = (int)BillType.Trash,
                Cost = 100m,
                DateOfPayment = DateTime.UtcNow,
                ServiceProviderName = "Bill"
            };

            await Client.PostAsJsonAsync(endpointAddress, command);

            return bill;
        }

        public void Dispose()
        {
            var bill = Fixture.WriteContext.Bills.FirstOrDefault();
            if (bill is null)
            {
                return;
            }

            Fixture.WriteContext.Bills.Remove(bill);
            Fixture.WriteContext.SaveChanges();
        }

        private void ProviderNew(Bill bill)
        {
            Fixture.WriteContext.Bills.Add(bill);
            Fixture.WriteContext.SaveChanges();
        }
    }
}
