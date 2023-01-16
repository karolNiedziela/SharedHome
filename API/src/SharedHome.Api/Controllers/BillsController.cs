using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.Bills.Commands.AddBill;
using SharedHome.Application.Bills.Commands.CancelBillPayment;
using SharedHome.Application.Bills.Commands.ChangeBillCost;
using SharedHome.Application.Bills.Commands.ChangeBillDateOfPayment;
using SharedHome.Application.Bills.Commands.DeleteBill;
using SharedHome.Application.Bills.Commands.PayForBill;
using SharedHome.Application.Bills.Commands.UpdateBill;
using SharedHome.Application.Bills.DTO;
using SharedHome.Application.Bills.Queries;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Api.Controllers
{
    public class BillsController : ApiController
    {
        /// <summary>
        /// Returns bill by id
        /// </summary>
        /// <returns>Bill</returns>
        [HttpGet(ApiRoutes.Bills.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<BillDto>>> GetBillAsync(Guid billId)
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
        public async Task<ActionResult<ApiResponse<List<BillDto>>>> GetBillsByMonthAndYearAsync([FromQuery] GetBillsByMonthAndYear query) 
        {
            var bills = await Mediator.Send(query);

            return Ok(bills);
        }

        /// <summary>
        /// Returns monthly expenses from year
        /// </summary>
        /// <returns>Months with expenses</returns>
        [HttpGet(ApiRoutes.Bills.GetMonthlyCost)]
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
        public async Task<IActionResult> AddBillAsync([FromBody] AddBillCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetBillAsync), new { billId = response.Data.Id }, response);
        }

        /// <summary>
        /// Pay for bill
        /// </summary>
        [HttpPatch(ApiRoutes.Bills.PayForBill)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PayForBillAsync([FromBody] PayForBillCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Change cost of bill
        /// </summary>
        [HttpPatch(ApiRoutes.Bills.ChangeBillCost)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeBillCostAsync([FromBody] ChangeBillCostCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Cancel payment of bill
        /// </summary>
        [HttpPatch(ApiRoutes.Bills.CancelPayment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelBillPaymentAsync([FromBody] CancelBillPaymentCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Change bill date of payment
        /// </summary>
        [HttpPatch(ApiRoutes.Bills.ChangeDateOfPayment)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeBillDateOfPaymentAsync([FromBody] ChangeBillDateOfPaymentCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Update bill
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBillAsync([FromBody] UpdateBillCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Delete bill
        /// </summary>
        [HttpDelete(ApiRoutes.Bills.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBillAsync(Guid billId)
        {
            await Mediator.Send(new DeleteBillCommand
            {
                BillId = billId
            });

            return NoContent();
        }
    }
}
