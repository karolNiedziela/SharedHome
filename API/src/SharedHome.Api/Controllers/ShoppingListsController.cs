using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.DTO;
using SharedHome.Application.ShoppingLists.Commands;
using SharedHome.Application.ShoppingLists.Queries;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Api.Controllers
{
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
        public async Task<ActionResult<Response<IEnumerable<ShoppingListDto>>>> GetAllByYearAndMonth([FromQuery] GetAllShoppingListByYearAndMonth query)
        {
            var shoppingLists = await Mediator.Send(query);

            return Ok(shoppingLists);
        }

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

        [HttpPut("{shoppingListId:int}/products/{productName}/purchase")]
        public async Task<IActionResult> PurchaseShoppingListProduct([FromBody] PurchaseProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{shoppingListId:int}/products/{productName}/cancelpurchase")]
        public async Task<IActionResult> CancePurchaseOfProduct([FromBody] CancelPurchaseOfProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{shoppingListId:int}/products/{productName}/changeprice")]
        public async Task<IActionResult> ChangePriceOfProduct([FromBody] ChangePriceOfProduct command)
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
