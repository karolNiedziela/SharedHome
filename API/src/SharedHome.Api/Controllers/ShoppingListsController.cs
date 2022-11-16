using Microsoft.AspNetCore.Mvc;
using SharedHome.Api.Constants;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingList;
using SharedHome.Application.ShoppingLists.Commands.AddShoppingListProducts;
using SharedHome.Application.ShoppingLists.Commands.CancelPurchaseOfProduct;
using SharedHome.Application.ShoppingLists.Commands.ChangePriceOfProduct;
using SharedHome.Application.ShoppingLists.Commands.DeleteManyShoppingListProducts;
using SharedHome.Application.ShoppingLists.Commands.DeleteShoppingList;
using SharedHome.Application.ShoppingLists.Commands.DeleteShoppingListProduct;
using SharedHome.Application.ShoppingLists.Commands.PurchaseProduct;
using SharedHome.Application.ShoppingLists.Commands.PurchaseProducts;
using SharedHome.Application.ShoppingLists.Commands.SetIsShoppingListDone;
using SharedHome.Application.ShoppingLists.Commands.UpdateShoppingList;
using SharedHome.Application.ShoppingLists.Commands.UpdateShoppingListProduct;
using SharedHome.Application.ShoppingLists.DTO;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Api.Controllers
{
    public class ShoppingListsController : ApiController
    {
        /// <summary>
        /// Returns shopping list by id
        /// </summary>
        /// <returns>Shopping list</returns>
        [HttpGet(ApiRoutes.ShoppingLists.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Response<ShoppingListDto>>> GetShoppingList(Guid shoppingListId)
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
        public async Task<IActionResult> AddShoppingList([FromBody] AddShoppingListCommand command)
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
        public async Task<IActionResult> AddShoppingListProduct([FromBody] AddShoppingListProductsCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Purchase shopping list product
        /// </summary>
        [HttpPatch(ApiRoutes.ShoppingLists.PurchaseShoppingListProduct)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PurchaseShoppingListProduct([FromBody] PurchaseProductCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut(ApiRoutes.ShoppingLists.PurchaseShoppingListProducts)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PurchaseShoppingListProducts([FromBody] PurchaseProductsCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Cancel purchase shopping list product
        /// </summary>
        [HttpPatch(ApiRoutes.ShoppingLists.CancelPurchaseOfProduct)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancePurchaseOfProduct([FromBody] CancelPurchaseOfProductCommand command)
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
        public async Task<IActionResult> ChangePriceOfProduct([FromBody] ChangePriceOfProductCommand command)
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
        public async Task<IActionResult> SetIsDone([FromBody]SetIsShoppingListDoneCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Update shopping list
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateShoppingListCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut(ApiRoutes.ShoppingLists.UpdateShoppingListProduct)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateShoppingListProductAsync([FromBody] UpdateShoppingListProductCommand command)
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
        public async Task<IActionResult> DeleteShoppingList(Guid shoppingListId)
        {
            await Mediator.Send(new DeleteShoppingListCommand
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
        public async Task<IActionResult> DeleteShoppingListProduct(Guid shoppingListId, string productName)
        {
            await Mediator.Send(new DeleteShoppingListProductCommand
            {
                ProductName = productName,
                ShoppingListId = shoppingListId
            });

            return NoContent();
        }

        [HttpDelete(ApiRoutes.ShoppingLists.DeleteManyShoppingListProducts)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteManyShoppingListProducts([FromBody]DeleteManyShoppingListProductsCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
