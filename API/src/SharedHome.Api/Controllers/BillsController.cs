using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.Bills.Commands;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
    public class BillsController : ApiController
    {
        [HttpGet("{billId:int}")]
        public async Task<ActionResult<Response<BillDto>>> GetBillAsync(int billId)
        {
            var bill = await Mediator.Send(new GetBill
            {
                Id = billId,
            });

            if (bill.Data is null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        [HttpGet]
        public async Task<ActionResult<List<BillDto>>> GetBillsByMonthAndYearAsync([FromQuery] GetBillsByMonthAndYear query) 
        {
            var bills = await Mediator.Send(query);

            return Ok(bills);
        }

        [HttpGet]
        [Route("monthlycost")]
        public async Task<ActionResult<List<BillMonthlyCostDto>>> GetMonthlyCostByYearAsync([FromQuery] GetMonthlyBillCostByYear query)
        {
            var groupedBillCostsByYear = await Mediator.Send(query);

            return Ok(groupedBillCostsByYear);
        }

        [HttpPost]
        public async Task<IActionResult> AddBillAsync([FromBody] AddBill command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{billId}/pay")]
        public async Task<IActionResult> PayForBillAsync([FromBody] PayForBill command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{billId}/changecost")]
        public async Task<IActionResult> ChangeBillCostAsync([FromBody] ChangeBillCost command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{billId}/cancelpayment")]
        public async Task<IActionResult> CancelBillPaymentAsync([FromBody] CancelBillPayment command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{billId}/changedateofpayment")]
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
