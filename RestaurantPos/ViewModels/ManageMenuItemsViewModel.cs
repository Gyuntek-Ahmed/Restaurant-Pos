using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private async Task EditMenuItemAsync(MenuItem menuItem)
        {
            await Shell.Current.DisplayAlert("Редактиране", menuItem.Name, "OK");
        }
    }
}
