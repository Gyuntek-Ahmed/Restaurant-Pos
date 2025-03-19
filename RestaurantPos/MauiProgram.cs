using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RestaurantPos.Data;
using RestaurantPos.Pages;
using RestaurantPos.ViewModels;

namespace RestaurantPos;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-Bold.ttf", "Poppinsbold");
            })
            .UseMauiCommunityToolkit();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services
			.AddSingleton<DatabaseService>()
			.AddSingleton<HomeViewModel>()
			.AddSingleton<MainPage>()
			.AddSingleton<OrdersViewModel>();

        return builder.Build();
	}
}
