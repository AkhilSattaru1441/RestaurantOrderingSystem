using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.DTOs.User;
using RestaurantOrderingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantOrderingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Create(OrderItemCreateDto dto)
        {
            var orderItem = await _orderItemService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = orderItem.OrderItemId },
                orderItem);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAll()
        {
            var orderItems = await _orderItemService.GetAllAsync();

            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orderItem = await _orderItemService.GetByIdAsync(id, userId);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItemCreateDto dto)
        {
            var orderItem = await _orderItemService.UpdateAsync(id, dto);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderItemService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new { message = "Order item deleted successfully." });
        }
    }
}

