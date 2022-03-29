using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.Bills.Commands;

namespace SharedHome.Api.Controllers
{
    public class BillsController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> AddBillAsync([FromBody] AddBill command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{billId}/pay")]
        public async Task<IActionResult> PayForBillAsync([FromBody] PayForBill command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{billId}/changecost")]
        public async Task<IActionResult> ChangeBillCostAsync([FromBody] ChangeBillCost command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{billId}/cancelpayment")]
        public async Task<IActionResult> CancelBillPaymentAsync([FromBody] CancelBillPayment command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{billId}/changedateofpayment")]
        public async Task<IActionResult> ChangeBillDateOfPaymentAsync([FromBody] ChangeBillDateOfPayment command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{billId}")]
        public async Task<IActionResult> DeleteBillAsync([FromBody] DeleteBill command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
