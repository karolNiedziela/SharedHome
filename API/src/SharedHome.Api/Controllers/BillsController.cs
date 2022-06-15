using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.Bills.Commands;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
    public class BillsController : ApiController
    {
        /// <summary>
        /// Returns bill by id
        /// </summary>
        /// <returns>Bill</returns>
        [HttpGet("{billId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Returns bills by month and year
        /// </summary>
        /// <returns>Bills from month and year</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BillDto>>> GetBillsByMonthAndYearAsync([FromQuery] GetBillsByMonthAndYear query) 
        {
            var bills = await Mediator.Send(query);

            return Ok(bills);
        }

        /// <summary>
        /// Returns monthly expenses from year
        /// </summary>
        /// <returns>Months with expenses</returns>
        [HttpGet("monthlycost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BillMonthlyCostDto>>> GetMonthlyCostByYearAsync([FromQuery] GetMonthlyBillCostByYear query)
        {
            var groupedBillCostsByYear = await Mediator.Send(query);

            return Ok(groupedBillCostsByYear);
        }

        /// <summary>
        /// Create new bill
        /// </summary>
        /// <returns>A newly created bill</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBillAsync([FromBody] AddBill command)
        {
            await Mediator.Send(command);

            return Created("", new { });
        }

        /// <summary>
        /// Pay for bill
        /// </summary>
        [HttpPatch("{billId}/pay")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PayForBillAsync([FromBody] PayForBill command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Change cost of bill
        /// </summary>
        [HttpPatch("{billId}/changecost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeBillCostAsync([FromBody] ChangeBillCost command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Cancel payment of bill
        /// </summary>
        [HttpPatch("{billId}/cancelpayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelBillPaymentAsync([FromBody] CancelBillPayment command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Change bill date of payment
        /// </summary>
        [HttpPatch("{billId}/changedateofpayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeBillDateOfPaymentAsync([FromBody] ChangeBillDateOfPayment command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBillAsync([FromBody] UpdateBill command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{billId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBillAsync([FromBody] DeleteBill command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
