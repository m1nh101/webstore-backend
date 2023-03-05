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
        public async Task<IActionResult> PostToOrder(string userId, int productVariantId)
        {
            var result = await _orderService.AddOrderAsync(userId, productVariantId);
            return Ok(result);
        }
    }
}
