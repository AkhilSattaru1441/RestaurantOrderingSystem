using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.DTOs.User
{
    public class OrderItemCreateDto
    {

        [Range(1, int.MaxValue)]
        public int OrderId { get; set; }

        [Range(1, int.MaxValue)]
        public int FoodId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
