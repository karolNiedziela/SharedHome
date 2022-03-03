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
        public async Task<IActionResult> AddShoppingList(AddShoppingList command)
        {
            await Mediator.Send(command);

            return Created("", new { });
        }

        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> AddShoppingListProduct(AddShoppingListProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("products/purchase")]
        public async Task<IActionResult> PurchaseShoppingListProduct(PurchaseProduct command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
