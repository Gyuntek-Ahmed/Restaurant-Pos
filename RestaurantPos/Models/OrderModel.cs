using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantPos.Data;

namespace RestaurantPos.Models
{
    public partial class OrderModel : ObservableObject
    {
        public long Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public int TotalItemsCount { get; set; }

        public decimal TotalAmountPaid { get; set; }

        public string PaymentMode { get; set; } = null!;

        public OrderItem[] Items { get; set; } = null!;

        [ObservableProperty]
        private bool isSelected;
    }
}
