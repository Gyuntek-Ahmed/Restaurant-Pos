using RestaurantPos.ViewModels;
using MenuItem = RestaurantPos.Data.MenuItem;

namespace RestaurantPos.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel homeViewModel;

        public MainPage(HomeViewModel homeViewModel)
        {
            InitializeComponent();
            this.homeViewModel = homeViewModel;
            BindingContext = homeViewModel;
            Initialize();
        }

        private async void Initialize()
        {
            await homeViewModel.InitializeAsync();
        }

        private async void CategoriesListControl_OnCategorySelected(Models.MenuCategoryModel category)
        {
            await homeViewModel.SelectCategoryCommand.ExecuteAsync(category);
        }

        private void MenuItemsListControl_OnSelectItem(MenuItem menuItem)
        {
            homeViewModel.AddToCartCommand.Execute(menuItem);
        }
    }
}
