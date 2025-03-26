using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPos.Data;
using RestaurantPos.Models;
using System.Collections.ObjectModel;

namespace RestaurantPos.ViewModels
{
    public partial class OrdersViewModel : ObservableObject
    {
        public DatabaseService databaseService;

        public OrdersViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public ObservableCollection<OrderModel> Orders { get; set; } = [];

        // This method will be called when the user clicks on the Place Order button
        // Returns true if the order is placed successfully
        public async Task<bool> PlaceOrderAsync(CartModel[] cartItems, bool isPaidOnline)
        {
            var orderItems = cartItems.Select(c => new OrderItem
            {
                Icon = c.Icon,
                ItemId = c.ItemId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.Quantity,
            }).ToArray();

            var orderModel = new OrderModel
            {
                OrderDate = DateTime.UtcNow,
                PaymentMode = isPaidOnline ? "Онлайн" : "В брой",
                TotalAmountPaid = cartItems.Sum(c => c.Amount),
                TotalItemsCount = cartItems.Length,
                Items = orderItems,
            };

            var errorMessage = await databaseService.PlaceOrderAsync(orderModel)!;

            if(!string.IsNullOrEmpty(errorMessage))
            {
                await Shell.Current.DisplayAlert("Грешка", errorMessage, "Ok");
                return false;
            }

            Orders.Add(orderModel);
            await Shell.Current.DisplayAlert("Успешно", "Поръчката е приета успешно", "OK");
            //await Toast.Make("Поръчката е приета успешно").Show();
            return true;
        }

        private bool isInitialized;

        [ObservableProperty]
        private bool isLoading;

        public async ValueTask InitializeAsync()
        {
            if (isInitialized)
                return;

            isInitialized = true;
            IsLoading = true;
            Orders.Clear();
            var dbOrders = await databaseService.GetOrdersAsync();

            var orders = dbOrders.Select(o => new OrderModel
            {
                Id = o.Id,
                OrderDate = DateTime.Now,
                PaymentMode = o.PaymentMode,
                TotalAmountPaid = o.TotalAmountPaid,
                TotalItemsCount = o.TotalItemsCount,
            });

            foreach (var order in orders)
            {
                Orders.Add(order);
            }

            IsLoading = false;
        }

        [ObservableProperty]
        private OrderItem[] orderItems = [];

        [RelayCommand]
        private async Task SelectOrderAsync(OrderModel? order)
        {
            var preSelectedOrder = Orders.FirstOrDefault(o => o.IsSelected);
            if (preSelectedOrder != null)
            {
                preSelectedOrder.IsSelected = false;

                if (preSelectedOrder.Id == order?.Id)
                {
                    OrderItems = [];
                    return;
                }
            }
            
            if (order == null || order.Id == 0)
            {
                OrderItems = [];
                return;
            }

            IsLoading = true;
            order.IsSelected = true;
            OrderItems = await databaseService.GetOrderItemsAsync(order.Id);
            IsLoading = false;
        }
    }
}
