using SQLite;

namespace RestaurantPos.Data
{
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public int TotalItemsCount { get; set; }

        public decimal TotalAmountPaid { get; set; }

        public string PaymentMode { get; set; } = null!;
    }
}
