using SharedHome.Api.Constants;
using SharedHome.Domain.Bills.Entities;
using SharedHome.IntegrationTests.Fixtures;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Linq;
using System.Net;
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
            var bill = BillProvider.Get();
            ProviderNew(bill);

            Authorize(bill.PersonId);

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.Bills.Get.Replace("{billId:int}", "1")}";

            var response = await Client.GetAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
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

            GC.SuppressFinalize(this);
        }

        private void ProviderNew(Bill bill)
        {
            Fixture.WriteContext.Bills.Add(bill);
            Fixture.WriteContext.SaveChanges();
        }
    }
}
