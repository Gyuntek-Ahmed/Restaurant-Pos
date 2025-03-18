using RestaurantPos.ViewModels;

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
    }
}
