using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int FoodId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
