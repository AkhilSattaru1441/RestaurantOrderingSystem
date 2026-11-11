using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; } // e.g., "Pending", "Completed", "Cancelled"
    }
}
