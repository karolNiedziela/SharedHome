using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
    [Authorize]
    public class ShoppingListsController : ApiController
    {
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Response<ShoppingListDto>>> GetShoppingList(int id)
        {
            var shoppingList = await Mediator.Send(new GetShoppingList
            {
                Id = id
            });

            if (shoppingList.Data is null)
            {
                return NotFound();
            }

            return Ok(shoppingList);
        }

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<ShoppingListDto>>>> GetAllByYearAndMonthAndIsDone([FromQuery] GetAllShoppingListsByYearAndMonthAndIsDone query)
        {
            var shoppingLists = await Mediator.Send(query);

            return Ok(shoppingLists);
        }

        [HttpGet]
        [Route("monthlycost")]
        public async Task<ActionResult<Response<List<ShoppingListMonthlyCostDto>>>> GetMonthlyCostByYearAsync([FromQuery] GetMonthlyShoppingListCostsByYear query)
        {
            var shoppingLists = await Mediator.Send(query);

            return Ok(shoppingLists);
        }

        /// <summary>
        /// Add new shopping list
        /// </summary>
        /// <response code="200">Add new shopping list</response>
        /// <response code="400">Unable to create shopping list due to validation error</response>
        [HttpPost]
        public async Task<IActionResult> AddShoppingList([FromBody] AddShoppingList command)
        {
            await Mediator.Send(command);

            return Created("", new { });
        }

        [HttpPut("{shoppingListId:int}/products")]
        public async Task<IActionResult> AddShoppingListProduct([FromBody] AddShoppingListProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{shoppingListId:int}/products/{productName}/purchase")]
        public async Task<IActionResult> PurchaseShoppingListProduct([FromBody] PurchaseProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{shoppingListId:int}/products/{productName}/cancelpurchase")]
        public async Task<IActionResult> CancePurchaseOfProduct([FromBody] CancelPurchaseOfProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{shoppingListId:int}/products/{productName}/changeprice")]
        public async Task<IActionResult> ChangePriceOfProduct([FromBody] ChangePriceOfProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPatch("{shoppingListId:int}/setisdone")]
        public async Task<IActionResult> SetIsDone([FromBody]SetIsShoppingListDone command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShoppingList([FromBody] DeleteShoppingList command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{shoppingListId:int}/products/{productName}")]
        public async Task<IActionResult> DeleteShoppingListProduct([FromBody] DeleteShoppingListProduct command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
