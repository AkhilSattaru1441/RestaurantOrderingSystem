using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.DTOs.User;
using RestaurantOrderingAPI.Models;
using RestaurantOrderingAPI.Repositories;

namespace RestaurantOrderingAPI.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository,
                                  IFoodItemRepository foodItemRepository,
                                  IOrderRepository orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _foodItemRepository = foodItemRepository;
            _orderRepository = orderRepository;
        }

        public async Task<OrderItemResponseDto> CreateAsync(OrderItemCreateDto dto)
        {
            var foodItem = await _foodItemRepository.GetByIdAsync(dto.FoodId);

            if (foodItem == null)
            {
                throw new Exception("Food item Not Found.");
            }
            var orderItem = new OrderItem
            {
                OrderId = dto.OrderId,
                FoodId = dto.FoodId,
                Quantity = dto.Quantity,
                Price = foodItem.Price
            };

            var createdOrderItem = await _orderItemRepository.CreateAsync(orderItem);

            var orderItems = await _orderItemRepository.GetByOrderIdAsync(dto.OrderId);
            var totalAmount = orderItems.Sum(Orderitem => Orderitem.Price * Orderitem.Quantity);
            var order = await _orderRepository.GetByIdAsync(dto.OrderId);
            if (order != null)
            {
                order.TotalAmount = totalAmount;

                await _orderRepository.UpdateAsync(order);
            }
            return new OrderItemResponseDto
            {
                OrderItemId = createdOrderItem.OrderItemId,
                OrderId = createdOrderItem.OrderId,
                FoodId = createdOrderItem.FoodId,
                Quantity = createdOrderItem.Quantity,
                Price = createdOrderItem.Price
            };
        }

        public async Task<List<OrderItemResponseDto>> GetAllAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();

            return orderItems.Select(o => new OrderItemResponseDto
            {
                OrderItemId = o.OrderItemId,
                OrderId = o.OrderId,
                FoodId = o.FoodId,
                Quantity = o.Quantity,
                Price = o.Price
            }).ToList();
        }

        public async Task<OrderItemResponseDto?> GetByIdAsync(int id, int userId)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);

            if (orderItem == null)
            {
                return null;
            }

            var order = await _orderRepository.GetByIdAsync(orderItem.OrderId);

            if (order == null || order.UserId != userId)
            {
                return null;
            }

            return new OrderItemResponseDto
            {

                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                FoodId = orderItem.FoodId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };
        }

        public async Task<OrderItemResponseDto?> UpdateAsync(int id, OrderItemCreateDto dto)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);

            if (orderItem == null)
            {
                return null;
            }

            orderItem.OrderId = dto.OrderId;
            orderItem.FoodId = dto.FoodId;
            orderItem.Quantity = dto.Quantity;

            var updatedOrderItem = await _orderItemRepository.UpdateAsync(orderItem);
           
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(updatedOrderItem.OrderId);

            var totalAmount = orderItems.Sum(item => item.Price * item.Quantity);

            var order = await _orderRepository.GetByIdAsync(updatedOrderItem.OrderId);

            if (order != null)
            {
                order.TotalAmount = totalAmount;

                await _orderRepository.UpdateAsync(order);
            }

            return new OrderItemResponseDto
            {
                OrderItemId = updatedOrderItem.OrderItemId,
                OrderId = updatedOrderItem.OrderId,
                FoodId = updatedOrderItem.FoodId,
                Quantity = updatedOrderItem.Quantity,
                Price = updatedOrderItem.Price
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderItem =await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                return false;
            }
            var orderId = orderItem.OrderId;
            var deleted = await _orderItemRepository.DeleteAsync(id);

            if (!deleted)
            {
                return false;
            }

            var orderItems = await _orderItemRepository.GetByOrderIdAsync(orderId);

            var totalAmount = orderItems
                .Sum(item => item.Price * item.Quantity);

            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order != null)
            {
                order.TotalAmount = totalAmount;

                await _orderRepository.UpdateAsync(order);
            }

            return true;
        }
    }
}

