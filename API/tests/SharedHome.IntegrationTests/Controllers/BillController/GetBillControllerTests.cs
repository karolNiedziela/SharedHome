using SharedHome.Api.Constants;
using SharedHome.Tests.Shared.Providers;
using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SharedHome.IntegrationTests.Controllers.BillController
{
    public class GetBillControllerTests : BillControllerTestsBase
    {
        public GetBillControllerTests(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_Should_Return_200_Status_Code()
        {
            var bill = BillProvider.Get(personId: TestDbInitializer.PersonId, billId: Guid.NewGuid());

            await _billSeed.AddAsync(bill);

            Authorize(bill.PersonId);

            var endpointAddress = $"{BaseAddress}/{ApiRoutes.Bills.Get.Replace("{billId}", bill.Id.Value.ToString())}";

            var response = await Client.GetAsync(endpointAddress);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
