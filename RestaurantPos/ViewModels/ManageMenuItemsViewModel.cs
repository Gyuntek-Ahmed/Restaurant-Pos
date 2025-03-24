using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RestaurantPos.Data;
using RestaurantPos.Models;
using MenuItem = RestaurantPos.Data.MenuItem;

namespace RestaurantPos.ViewModels
{
    public partial class ManageMenuItemsViewModel : ObservableObject
    {
        private readonly DatabaseService databaseService;

        public ManageMenuItemsViewModel(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        [ObservableProperty]
        public MenuCategoryModel[] categories = [];

        [ObservableProperty]
        public MenuItem[] menuItems = [];

        [ObservableProperty]
        private MenuCategoryModel? selectedCategory = null;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private MenuItemModel menuItemModel = new();

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

            SetEmptyCategoriesToItem();
            IsLoading = false;
        }

        private void SetEmptyCategoriesToItem()
        {
            MenuItemModel.Categories.Clear();

            foreach (var category in Categories)
            {
                var categoryOfItem = new MenuCategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Icon = category.Icon,
                    IsSelected = false
                };
                MenuItemModel.Categories.Add(categoryOfItem);
            }
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
        private async Task EditMenuItemAsync(MenuItem menuItem)
        {
            //await Shell.Current.DisplayAlert("Редактиране", menuItem.Name, "OK");
            var menuItemModel = new MenuItemModel
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Icon = menuItem.Icon,
                Description = menuItem.Description,
                Price = menuItem.Price
            };

            var itemCategories = await databaseService.GetCategoriesOfMenuItemAsync(menuItem.Id);

            foreach (var category in Categories)
            {
                var categoryOfItem = new MenuCategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Icon = category.Icon,
                };

                if (itemCategories.Any(c => c.Id == category.Id))
                    categoryOfItem.IsSelected = true;
                else
                    categoryOfItem.IsSelected = false;

                menuItemModel.Categories.Add(categoryOfItem);
            }

            MenuItemModel = menuItemModel;
        }

        [RelayCommand]
        private void Cancel()
        {
            MenuItemModel = new();
            SetEmptyCategoriesToItem();
        }

        [RelayCommand]
        private async Task SaveMenuItemAsync(MenuItemModel model)
        {
            IsLoading = true;

            var errorMessage = await databaseService.SaveMenuItemAsync(model);

            if (errorMessage != null)
                await Shell.Current.DisplayAlert("Грешка", errorMessage, "OK");
            else
            {
                await Shell.Current.DisplayAlert("Успешно", "Артикулът е записан успешно.", "OK");
                HandleMenuItemChanged(model);
                WeakReferenceMessenger.Default.Send(MenuItemChangeMessage.From(model));
                Cancel();
            }

            IsLoading = false;
        }

        private void HandleMenuItemChanged(MenuItemModel model)
        {
            var menuItem = MenuItems.FirstOrDefault(i => i.Id == model.Id);

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
        }
    }
}