using RestaurantPos.ViewModels;
using System.Threading.Tasks;
using MenuItem = RestaurantPos.Data.MenuItem;

namespace RestaurantPos.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel homeViewModel;
        private readonly SettingsViewModel settingsViewModel;

        public MainPage(HomeViewModel homeViewModel, SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            this.homeViewModel = homeViewModel;
            this.settingsViewModel = settingsViewModel;
            BindingContext = homeViewModel;
            Initialize();
        }

        private async void Initialize()
        {
            await homeViewModel.InitializeAsync();
        }

        protected override async void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            await settingsViewModel.InitializeAsync();
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
