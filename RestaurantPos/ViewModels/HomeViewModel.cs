using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantPos.Data;
using RestaurantPos.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MenuItem = RestaurantPos.Data.MenuItem;

namespace RestaurantPos.ViewModels
{
    public partial class HomeViewModel : ObservableObject, IRecipient<MenuItemChangeMessage>
    {
        private readonly DatabaseService databaseService;
        private readonly OrdersViewModel ordersViewModel;
        private readonly SettingsViewModel settingsViewModel;
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

        [ObservableProperty]
        private string name = "Гост";

        public HomeViewModel(DatabaseService databaseService, OrdersViewModel ordersViewModel, SettingsViewModel settingsViewModel)
        {
            this.databaseService = databaseService;
            this.ordersViewModel = ordersViewModel;
            this.settingsViewModel = settingsViewModel;
            CartItems.CollectionChanged += CartItems_CollectionChanged;

            WeakReferenceMessenger.Default.Register<MenuItemChangeMessage>(this);
            WeakReferenceMessenger.Default.Register<NameChangedMessage>(this, (recepient, message) => Name = message.Value);

            TaxPercentage = settingsViewModel.GetTaxPercentage();
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
        private async Task ClearCartAsync()
        {
            if (await Shell.Current.DisplayAlert
                ("Изчистване", "Желаете ли да премахнете всички продукти от кошницата?", "ДА", "НЕ"))
                CartItems.Clear();
        }

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

                if (enteredTaxPercentage > 100)
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
                settingsViewModel.SetTaxPercentage(enteredTaxPercentage);
            }
        }

        [RelayCommand]
        private async Task PlaceOrderAsync(bool isPaidOnline)
        {
            IsLoading = true;
            if(await ordersViewModel.PlaceOrderAsync([.. CartItems], isPaidOnline))
                CartItems.Clear();

            IsLoading = false;
        }

        public void Receive(MenuItemChangeMessage message)
        {
            var model = message.Value;
            var menuItem = MenuItems.FirstOrDefault(m => m.Id == model.Id);

            if (menuItem != null)
            {
                if (!model.SelectedCategories.Any(c => c.Id == SelectedCategory!.Id))
                {
                    MenuItems = [.. MenuItems.Where(m => m.Id != model.Id)];
                    return;
                }

                menuItem.Price = model.Price;
                menuItem.Description = model.Description;
                menuItem.Name = model.Name;
                menuItem.Icon = model.Icon;

                MenuItems = [.. MenuItems];
            }
            else if (model.SelectedCategories.Any(c => c.Id == SelectedCategory!.Id))
            {
                var newMenuItem = new MenuItem
                {
                    Id = model.Id,
                    Name = model.Name,
                    Icon = model.Icon,
                    Description = model.Description,
                    Price = model.Price
                };

                MenuItems = [.. MenuItems, newMenuItem];
            }

            var cartItem = CartItems.FirstOrDefault(c => c.ItemId == model.Id);

            if (cartItem != null)
            {
                cartItem.Name = model.Name;
                cartItem.Icon = model.Icon;
                cartItem.Price = model.Price;

                var itemIndex = CartItems.IndexOf(cartItem);

                CartItems[itemIndex] = cartItem;
            }
        }
    }
}