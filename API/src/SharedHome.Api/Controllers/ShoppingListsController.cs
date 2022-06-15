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
        /// <summary>
        /// Returns shopping list by id
        /// </summary>
        /// <returns>Shopping list</returns>
        [HttpGet("{shoppingListId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Returns shopping list by year, month and status
        /// </summary>
        /// <returns>Shopping lists</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<IEnumerable<ShoppingListDto>>>> GetAllByYearAndMonthAndIsDone([FromQuery] GetAllShoppingListsByYearAndMonthAndIsDone query)
        {
            var shoppingLists = await Mediator.Send(query);

            return Ok(shoppingLists);
        }

        /// <summary>
        /// Returns monthly expenses from year
        /// </summary>
        /// <returns>Months with expenses</returns>
        [HttpGet("monthlycost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response<List<ShoppingListMonthlyCostDto>>>> GetMonthlyCostByYearAsync([FromQuery] GetMonthlyShoppingListCostsByYear query)
        {
            var shoppingLists = await Mediator.Send(query);

            return Ok(shoppingLists);
        }

        /// <summary>
        /// Create new shopping list
        /// </summary>
        /// <returns>A newly created shopping list</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddShoppingList([FromBody] AddShoppingList command)
        {
            await Mediator.Send(command);

            return Created("", new { });
        }

        /// <summary>
        /// Add shopping list product to shopping list
        /// </summary>
        [HttpPut("{shoppingListId:int}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddShoppingListProduct([FromBody] AddShoppingListProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Purchase shopping list product
        /// </summary>
        [HttpPatch("{shoppingListId:int}/products/{productName}/purchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PurchaseShoppingListProduct([FromBody] PurchaseProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Cancel purchase shopping list product
        /// </summary>
        [HttpPatch("{shoppingListId:int}/products/{productName}/cancelpurchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancePurchaseOfProduct([FromBody] CancelPurchaseOfProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Change price of shopping list product
        /// </summary>
        [HttpPatch("{shoppingListId:int}/products/{productName}/changeprice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePriceOfProduct([FromBody] ChangePriceOfProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Set status of shopping list
        /// </summary>
        [HttpPatch("{shoppingListId:int}/setisdone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetIsDone([FromBody]SetIsShoppingListDone command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Update shopping list
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateShoppingList command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Delete shopping list
        /// </summary>
        [HttpDelete("{shoppingListId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShoppingList([FromBody] DeleteShoppingList command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete shopping list product
        /// </summary>
        [HttpDelete("{shoppingListId:int}/products/{productName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShoppingListProduct([FromBody] DeleteShoppingListProduct command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
