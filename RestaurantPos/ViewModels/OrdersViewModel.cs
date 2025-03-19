using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantPos.Data;
using RestaurantPos.Models;

namespace RestaurantPos.ViewModels
{
    public partial class OrdersViewModel : ObservableObject
    {
        public DatabaseService databaseService { get; }

        public OrdersViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

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
                PaymentMode = isPaidOnline ? "Online" : "Cash",
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

            await Shell.Current.DisplayAlert("Успешно", "Поръчката е приета успешно", "OK");
            //await Toast.Make("Поръчката е приета успешно").Show();
            return true;
        }
    }
}
