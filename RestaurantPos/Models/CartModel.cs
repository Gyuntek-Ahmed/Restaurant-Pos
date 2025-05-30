﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace RestaurantPos.Models
{
    public partial class CartModel : ObservableObject
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount))]
        private int quantity;

        public decimal Amount => Price * Quantity;
    }
}
