using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
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
        [HttpGet(ApiRoutes.ShoppingLists.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<ShoppingListDto>>> GetShoppingList(int shoppingListId)
        {
            var shoppingList = await Mediator.Send(new GetShoppingList
            {
                Id = shoppingListId
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
        [HttpGet(ApiRoutes.ShoppingLists.GetMonthlyCost)]
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
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetShoppingList), new { shoppingListId = response.Data.Id}, response);
        }

        /// <summary>
        /// Add shopping list product to shopping list
        /// </summary>
        [HttpPut(ApiRoutes.ShoppingLists.AddShoppingListProduct)]
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
        [HttpPatch(ApiRoutes.ShoppingLists.PurchaseShoppingList)]
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
        [HttpPatch(ApiRoutes.ShoppingLists.CancelPurchaseOfProduct)]
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
        [HttpPatch(ApiRoutes.ShoppingLists.ChangePriceOfProduct)]
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
        [HttpPatch(ApiRoutes.ShoppingLists.SetIsDone)]
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
        [HttpDelete(ApiRoutes.ShoppingLists.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShoppingList(int shoppingListId)
        {
            await Mediator.Send(new DeleteShoppingList
            {
                ShoppingListId = shoppingListId
            });

            return NoContent();
        }

        /// <summary>
        /// Delete shopping list product
        /// </summary>
        [HttpDelete(ApiRoutes.ShoppingLists.DeleteShoppingListProduct)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShoppingListProduct(int shoppingListId, string productName)
        {
            await Mediator.Send(new DeleteShoppingListProduct
            {
                ProductName = productName,
                ShoppingListId = shoppingListId
            });

            return NoContent();
        }
    }
}
