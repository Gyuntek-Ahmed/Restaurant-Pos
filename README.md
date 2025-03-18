# RestaurantPos

RestaurantPos is a restaurant management application developed with .NET MAUI. The app supports various platforms including Android, iOS, MacCatalyst and Windows.

## Features

- Menu management with categories and items
- Add items to cart
- Calculate total and tax
- Database initialization

## Installation

1. Clone the repository:
    

2. Open the project in Visual Studio 2022.
3. Make sure you have the necessary SDK and tools for .NET MAUI installed.

## Using

1. Launch the application from Visual Studio.
2. The application will initialize the database the first time it is started.
3. Use the interface to manage the menu and add items to the cart.

## Dependencies

- .NET 9
- CommunityToolkit.Maui
- CommunityToolkit.Mvvm
- sqlite-net-pcl
- SQLitePCLRaw.bundle_green

## Project structure

- `App.xaml.cs`: The main application class that initializes the database and creates the main window.
- `ViewModels/HomeViewModel.cs`: ViewModel class for the main page that manages the application data and logic.
- `Data/SeedData.cs`: Class for initializing sample data in the database.

## License

This project is licensed under the MIT license. See the file [LICENSE](LICENSE) for more information.
