using RestaurantPos.ViewModels;

namespace RestaurantPos.Pages;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel ordersViewModel;

	public OrdersPage(OrdersViewModel ordersViewModel)
	{
		InitializeComponent();
        this.ordersViewModel = ordersViewModel;
        BindingContext = this.ordersViewModel;
        InitializeViewModelAsync();
    }

    private async void InitializeViewModelAsync()
        => await ordersViewModel.InitializeAsync();
}