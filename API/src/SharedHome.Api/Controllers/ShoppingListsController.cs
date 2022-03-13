using Microsoft.AspNetCore.Mvc;
using SharedHome.Application.ShoppingLists.Commands;

namespace SharedHome.Api.Controllers
{
    public class ShoppingListsController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetShoppingList()
        {
            return Ok();
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

        [HttpPost("{shoppingListId:int}/products/{name}/purchase")]
        public async Task<IActionResult> PurchaseShoppingListProduct([FromBody] PurchaseProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{shoppingListId:int}/products/{name}/cancelpurchase")]
        public async Task<IActionResult> CancePurchaseOfProduct([FromBody] CancelPurchaseOfProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{shoppingListId:int}/products/{name}/changeprice")]
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

        [HttpDelete("{shoppingListId:int}/products/{name}")]
        public async Task<IActionResult> DeleteShoppingListProduct([FromBody] DeleteShoppingListProduct command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
