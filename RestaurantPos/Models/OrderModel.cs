using RestaurantPos.Data;

namespace RestaurantPos.Models
{
    public class OrderModel
    {
        public long Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public int TotalItemsCount { get; set; }

        public decimal TotalAmountPaid { get; set; }

        public string PaymentMode { get; set; } = null!;

        public OrderItem[] Items { get; set; } = null!;
    }
}
