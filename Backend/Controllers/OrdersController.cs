using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantOrderingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(OrderCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = await _orderService.CreateAsync(dto, userId);

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.OrderId },
                order);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();

            return Ok(orders);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            Console.WriteLine("GET ORDER BY ID CONTROLLER REACHED");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = await _orderService.GetByIdAsync(id, userId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Update(int id, OrderCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = await _orderService.UpdateAsync(id, dto,  userId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _orderService.DeleteAsync(id,userId);

            if (!result)
            {
                return NotFound();
            }
             
             
            return Ok(new { message = "Order deleted successfully." });
        }
    }
}