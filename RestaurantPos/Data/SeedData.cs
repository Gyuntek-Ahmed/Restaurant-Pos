namespace RestaurantPos.Data
{
    public static class SeedData
    {
        public static List<MenuCategory> GetMenuCategories()
        {
            return new List<MenuCategory>
            {
                new() { Id = 1, Name = "Напитки", Icon = "drink.png" },
                new() { Id = 2, Name = "Основно ястие", Icon = "masala_dosa.png" },
                new() { Id = 3, Name = "Закуски", Icon = "snacks.png" },
                new() { Id = 4, Name = "Десерти", Icon = "cake.png" },
                new() { Id = 5, Name = "Бързо Хранене", Icon = "fast_food.png" },
            };
        }

        public static List<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
                new() { Id = 1, Name = "Бира", Icon = "beer.png", Description = "Ледено Студена Бира", Price = 2.99m },
                new() { Id = 2, Name = "Пица", Icon = "pizza.png", Description = "Пица със сирене и доматен сос", Price = 5.99m },
                new() { Id = 3, Name = "Пиле", Icon = "roasted_chicken.png", Description = "Печено пиле", Price = 7.99m },
                new() { Id = 4, Name = "Паста", Icon = "pasta.png", Description = "Паста със сос", Price = 4.99m },
                new() { Id = 5, Name = "Шопска Салата", Icon = "salad_bowl.png", Description = "Салата домати, краставици, сирене...", Price = 3.99m },
                new() { Id = 6, Name = "Кекс", Icon = "cake.png", Description = "Шоколадов кекс", Price = 1.99m },
                new() { Id = 7, Name = "Кафе", Icon = "coffee.png", Description = "Чаша кафе", Price = 1.49m },
                new() { Id = 8, Name = "Чай", Icon = "bowl_hot_regular_24.png", Description = "Чаша чай", Price = 1.29m },
                new() { Id = 9, Name = "Хамбургер", Icon = "hamburger.png", Description = "Хамбургер с картофи", Price = 3.99m },
                new() { Id = 10, Name = "Пиле", Icon = "fried_chicken.png", Description = "Пилешко бутче", Price = 9.99m },
                new() { Id = 11, Name = "Гръцка Салата", Icon = "salad_plate.png", Description = "Салата по гръцки", Price = 3.99m },
                new() { Id = 12, Name = "Шоколад", Icon = "chocolate.png", Description = "Шоколадови кубчета", Price = 3.99m },
                new() { Id = 13, Name = "Коктейл", Icon = "cocktail.png", Description = "Разхлаждащ коктейл", Price = 1.49m },
                new() { Id = 14, Name = "Торта", Icon = "cupcake.png", Description = "Плодова торта", Price = 10.99m },
                new() { Id = 15, Name = "Поничка", Icon = "donut.png", Description = "Поничка с малинов пълнеж", Price = 1.49m },
                new() { Id = 16, Name = "Енергийна Напитка", Icon = "energy_drink.png", Description = "Енергийна напитка Hell", Price = 0.99m },
                new() { Id = 17, Name = "Бургер Меню", Icon = "burger_fries_combo.png", Description = "Бургер с картофи и напитка", Price = 5.99m },
                new() { Id = 18, Name = "Хот-Дог", Icon = "hotdog.png", Description = "Хот-Дог с кебабче", Price = 1.99m },
                new() { Id = 19, Name = "Риба С Картофи", Icon = "fish_and_chips.png", Description = "Риба и пържени картофи", Price = 7.99m },
                new() { Id = 20, Name = "Риба", Icon = "fish.png", Description = "Прясна риба", Price = 2.99m },
                new() { Id = 21, Name = "Яйца", Icon = "fried_egg.png", Description = "Пържени яйца", Price = 0.99m },
                new() { Id = 22, Name = "Картофи", Icon = "french_fries.png", Description = "Пържени картофи", Price = 0.99m },
                new() { Id = 23, Name = "Ориз", Icon = "fried_rice.png", Description = "Ориз по китайски", Price = 1.99m },
                new() { Id = 24, Name = "Студени Напитки", Icon = "soft_drink.png", Description = "Кола, Фанта, Спрайт", Price = 1.49m },
                new() { Id = 25, Name = "Чаша Студена Напитка", Icon = "soda.png", Description = "Кола, Фанта, Спрайт", Price = 0.49m },
                new() { Id = 26, Name = "Спагети", Icon = "spaghetti.png", Description = "Спагети Болонезе", Price = 4.49m },
                new() { Id = 27, Name = "Суши", Icon = "sushi.png", Description = "Суши по оригинална рецепта", Price = 8.49m },
                new() { Id = 28, Name = "Сладолед", Icon = "ice_cream.png", Description = "Сладоледени топки с допинг", Price = 2.49m },
                new() { Id = 29, Name = "Дюнер", Icon = "kebab.png", Description = "Пилешки или телешки дюнер", Price = 5.99m },
                new() { Id = 30, Name = "Вкусен Рак", Icon = "lobster.png", Description = "Рак с гарнитура", Price = 9.99m },
                new() { Id = 31, Name = "Манго", Icon = "mango.png", Description = "Купа с нарязани парчета манго", Price = 1.99m },
                new() { Id = 32, Name = "Купа Чипс", Icon = "masala_dosa.png", Description = "Купа чипс с различни видове сос", Price = 2.99m },
                new() { Id = 33, Name = "Инстантни Спагети", Icon = "noodles.png", Description = "Инстантни спагети с различни вкусове", Price = 0.99m },
                new() { Id = 34, Name = "Натурален Сок", Icon = "orange_juice.png", Description = "Натурален сок различни видове", Price = 0.99m },
                new() { Id = 35, Name = "Палачинка", Icon = "pancakes.png", Description = "Палачинка с мед и орехи", Price = 1.99m },
                new() { Id = 36, Name = "Пай", Icon = "pie.png", Description = "Ябълков пай", Price = 0.99m },
                new() { Id = 37, Name = "Сандвич", Icon = "sandwich.png", Description = "Сандвич с пуешко филе", Price = 2.99m },
                new() { Id = 38, Name = "Снак", Icon = "snacks.png", Description = "Снак с лешници", Price = 0.99m },
                new() { Id = 39, Name = "Тотрила", Icon = "wrap.png", Description = "Тотрила с пилешко", Price = 2.99m },
            };
        }

        public static List<MenuItemCategoryMapping> GetMenuItemCategoryMappings()
        {
            return new List<MenuItemCategoryMapping>
            {
#region Напитки
                new() { MenuCategoryId = 1, MenuItemId = 1 },
                new() { MenuCategoryId = 1, MenuItemId = 7 },
                new() { MenuCategoryId = 1, MenuItemId = 8 },
                new() { MenuCategoryId = 1, MenuItemId = 13 },
                new() { MenuCategoryId = 1, MenuItemId = 16 },
                new() { MenuCategoryId = 1, MenuItemId = 24 },
                new() { MenuCategoryId = 1, MenuItemId = 25 },
                new() { MenuCategoryId = 1, MenuItemId = 34 },
                #endregion

#region Основни Ястия
                new() { MenuCategoryId = 2, MenuItemId = 2 },
                new() { MenuCategoryId = 2, MenuItemId = 3 },
                new() { MenuCategoryId = 2, MenuItemId = 4 },
                new() { MenuCategoryId = 2, MenuItemId = 5 },
                new() { MenuCategoryId = 2, MenuItemId = 10 },
                new() { MenuCategoryId = 2, MenuItemId = 11 },
                new() { MenuCategoryId = 2, MenuItemId = 19 },
                new() { MenuCategoryId = 2, MenuItemId = 20 },
                new() { MenuCategoryId = 2, MenuItemId = 23 },
                new() { MenuCategoryId = 2, MenuItemId = 26 },
                new() { MenuCategoryId = 2, MenuItemId = 27 },
                new() { MenuCategoryId = 2, MenuItemId = 30 },
                #endregion

#region Скаксове
                new() { MenuCategoryId = 3, MenuItemId = 32 },
                new() { MenuCategoryId = 3, MenuItemId = 38 },
                #endregion

#region Десерти
                new() { MenuCategoryId = 4, MenuItemId = 6 },
                new() { MenuCategoryId = 4, MenuItemId = 12 },
                new() { MenuCategoryId = 4, MenuItemId = 14 },
                new() { MenuCategoryId = 4, MenuItemId = 15 },
                new() { MenuCategoryId = 4, MenuItemId = 28 },
                new() { MenuCategoryId = 4, MenuItemId = 31 },
                new() { MenuCategoryId = 4, MenuItemId = 35 },
                new() { MenuCategoryId = 4, MenuItemId = 36 },
                #endregion

#region Бързо Хранене
                new() { MenuCategoryId = 5, MenuItemId = 9 },
                new() { MenuCategoryId = 5, MenuItemId = 17 },
                new() { MenuCategoryId = 5, MenuItemId = 18 },
                new() { MenuCategoryId = 5, MenuItemId = 21 },
                new() { MenuCategoryId = 5, MenuItemId = 22 },
                new() { MenuCategoryId = 5, MenuItemId = 29 },
                new() { MenuCategoryId = 5, MenuItemId = 33 },
                new() { MenuCategoryId = 5, MenuItemId = 37 },
                new() { MenuCategoryId = 5, MenuItemId = 39 },
#endregion
            };
        }
    }
}
