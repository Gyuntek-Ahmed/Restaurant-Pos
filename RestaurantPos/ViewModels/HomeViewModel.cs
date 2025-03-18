using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPos.Data;
using RestaurantPos.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MenuItem = RestaurantPos.Data.MenuItem;

namespace RestaurantPos.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DatabaseService databaseService;

        [ObservableProperty]
        public MenuCategoryModel[] categories = [];

        [ObservableProperty]
        public MenuItem[] menuItems = [];

        [ObservableProperty]
        private MenuCategoryModel? selectedCategory = null;

        public ObservableCollection<CartModel> CartItems { get; set; } = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TaxAmount))]
        [NotifyPropertyChangedFor(nameof(Total))]
        private decimal subTotal;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TaxAmount))]
        private int taxPercentage;

        public decimal TaxAmount => (SubTotal * TaxPercentage) / 100;
        public decimal Total => SubTotal + TaxAmount;

        public HomeViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
            CartItems.CollectionChanged += CartItems_CollectionChanged;
        }

        private void CartItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            // We need to recalculate the amounts whenever the cart items change
            // It will be executed whenever we are adding or removing items from the cart
            // or Clearing the cart
            // or changing the quantity of the items
            RecalculateAmounts();
        }

        public bool isInitialized;

        public async ValueTask InitializeAsync()
        {
            if (isInitialized)
                return; // Already initialized
            isInitialized = true;
            IsLoading = true;

            Categories = (await databaseService.GetMenuCategoriesAsync())
                .Select(MenuCategoryModel.FromEntity)
                .ToArray();

            Categories[0].IsSelected = true;
            SelectedCategory = Categories[0];

            MenuItems = await databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);

            IsLoading = false;
        }

        [RelayCommand]
        private async Task SelectCategoryAsync(MenuCategoryModel menuCategoryModel)
        {
            if (SelectedCategory!.Id == menuCategoryModel.Id)
                return; // Already selected
            IsLoading = true;

            var existingSelectedCategory = Categories.First(c => c.IsSelected);
            existingSelectedCategory.IsSelected = false;

            var newSelectedCategory = Categories.First(c => c.Id == menuCategoryModel.Id);
            newSelectedCategory.IsSelected = true;

            SelectedCategory = newSelectedCategory;
            MenuItems = await databaseService.GetMenuItemsByCategoryAsync(SelectedCategory.Id);

            IsLoading = false;
        }

        [RelayCommand]
        private void AddToCart(MenuItem menuItem)
        {
            var cartItem = CartItems.FirstOrDefault(c => c.ItemId == menuItem.Id);
            if (cartItem == null)
            {
                cartItem = new CartModel
                {
                    ItemId = menuItem.Id,
                    Name = menuItem.Name,
                    Icon = menuItem.Icon,
                    Price = menuItem.Price,
                    Quantity = 1
                };
                CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
                RecalculateAmounts();
            }
        }

        [RelayCommand]
        private void IncreaseQuantity(CartModel cartItem)
        {
            cartItem.Quantity++;
            RecalculateAmounts();
        }

        [RelayCommand]
        private void DecreaseQuantity(CartModel cartItem)
        {
            if (cartItem != null)
            {
                cartItem.Quantity--;
                if (cartItem.Quantity == 0)
                    CartItems.Remove(cartItem);
                else
                    RecalculateAmounts();
            }
        }

        [RelayCommand]
        private void RemoveItemFromCart(CartModel cartItem) => CartItems.Remove(cartItem);

        [RelayCommand]
        private void ClearCart() => CartItems.Clear();

        private void RecalculateAmounts()
        {
            SubTotal = CartItems.Sum(c => c.Amount);
        }

        [RelayCommand]
        private async Task TaxPercentageClickAsync()
        {
            var result = await
                Shell
                .Current
                .DisplayPromptAsync("Процент ДДС",
                                    "Въведете приложимия данъчен процент",
                                    placeholder: "10",
                                    initialValue: TaxPercentage.ToString());

            if (!string.IsNullOrEmpty(result))
            {
                if (!int.TryParse(result, out var enteredTaxPercentage))
                {
                    await Shell.Current.DisplayAlert("Грешка", "Моля въведете валиден данъчен процент", "OK");
                    return;
                }

                if(enteredTaxPercentage > 100)
                {
                    await Shell.Current.DisplayAlert("Грешка", "Моля въведете данъчен процент по-малък от 100", "OK");
                    return;
                }

                if (enteredTaxPercentage < 0)
                {
                    await Shell.Current.DisplayAlert("Грешка", "Моля въведете данъчен процент по-голям от 0", "OK");
                    return;
                }

                TaxPercentage = enteredTaxPercentage;
            }
        }
    }
}