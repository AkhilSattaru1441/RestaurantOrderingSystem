using Microsoft.Extensions.Configuration.UserSecrets;
using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.Models;
using RestaurantOrderingAPI.Repositories;

namespace RestaurantOrderingAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }
        public async Task<OrderResponseDto> CreateAsync(OrderCreateDto dto,int userId)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
                Status = "Pending"
            };

            var createdOrder = await _orderRepository.CreateAsync(order);

            return new OrderResponseDto
            {
                OrderId = createdOrder.OrderId,
                UserId = createdOrder.UserId,
                OrderDate = createdOrder.OrderDate,
                TotalAmount = createdOrder.TotalAmount,
                Status = createdOrder.Status
            };
        }
        public async Task<List<OrderResponseDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(o => new OrderResponseDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status
            }).ToList();
        }
        public async Task<OrderResponseDto?> GetByIdAsync(int id, int userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return null;
            }

            if (order.UserId != userId)
            {
                return null;
            }



            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status
            };
        }
        public async Task<OrderResponseDto?> UpdateAsync(int id, OrderCreateDto dto,int userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return null;
            }

            if ( order.UserId != userId)
            {
                return null;
            }


            var updatedOrder = await _orderRepository.UpdateAsync(order);

            return new OrderResponseDto
            {
                OrderId = updatedOrder.OrderId,
                UserId = updatedOrder.UserId,
                OrderDate = updatedOrder.OrderDate,
                TotalAmount = updatedOrder.TotalAmount,
                Status = updatedOrder.Status
            };
        }
        public async Task<bool> DeleteAsync(int id,int userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return false;
            }

            if (order.UserId != userId)
            {
                return false;
            }
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(id);

            foreach (var item in orderItems)
            {
                await _orderItemRepository.DeleteAsync(item.OrderItemId);
            }

            return await _orderRepository.DeleteAsync(id);
        }
    }

}
