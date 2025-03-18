using SQLite;

namespace RestaurantPos.Data
{
    public class DatabaseService : IAsyncDisposable
    {
        private readonly SQLiteAsyncConnection connection;
        public DatabaseService()
        {
            var dbPath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RestaurantPos.db3");

            connection = new SQLiteAsyncConnection(dbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }

        public async Task InitializeDatabaseAsync()
        {
            await connection.CreateTableAsync<MenuCategory>();
            await connection.CreateTableAsync<MenuItem>();
            await connection.CreateTableAsync<MenuItemCategoryMapping>();
            await connection.CreateTableAsync<Order>();
            await connection.CreateTableAsync<OrderItem>();

            await SeedDataAsync();
        }

        private async Task SeedDataAsync()
        {
            var firstCategory = await connection.Table<MenuCategory>().FirstOrDefaultAsync();

            if (firstCategory != null)
                return; // Already seeded

            var categories = SeedData.GetMenuCategories();
            var menuItems = SeedData.GetMenuItems();
            var mappings = SeedData.GetMenuItemCategoryMappings();

            await connection.InsertAllAsync(categories);
            await connection.InsertAllAsync(menuItems);
            await connection.InsertAllAsync(mappings);
        }

        public async Task<MenuCategory[]> GetMenuCategoriesAsync()
            => await connection.Table<MenuCategory>().ToArrayAsync();

        public async Task<MenuItem[]> GetMenuItemsByCategoryAsync(int categoryId)
        {
            var query = @"
SELECT menu.*
FROM MenuItem AS menu
INNER JOIN MenuItemCategoryMapping AS mapping
ON menu.Id = mapping.MenuItemId
WHERE mapping.MenuCategoryId = ?
";

            var menuItems = await connection.QueryAsync<MenuItem>(query, categoryId);
            return [..menuItems];
        }

        public async ValueTask DisposeAsync()
        {
            if (connection != null)
                await connection.CloseAsync();
        }
    }
}
