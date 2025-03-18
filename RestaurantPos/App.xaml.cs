using RestaurantPos.Data;

namespace RestaurantPos
{
    public partial class App : Application
    {
        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            Task.Run(async () =>
                await databaseService.InitializeDatabaseAsync())
                .GetAwaiter()
                .GetResult();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}