using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Application.Common.Contracts;
using WebStore.Domain.Entities;

namespace WebStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("{productVariantId:int}")]
        public async Task<IActionResult> AddItem(int productVariantId)
        {
            var result = await _orderService.AddItemToOrderAsync(productVariantId);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id)
        {
            await _orderService.IncreaseQuantity(id);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            return Ok();
        }
    }
}
