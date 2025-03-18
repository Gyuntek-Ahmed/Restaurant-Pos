using SQLite;

namespace RestaurantPos.Data
{
    public class MenuCategory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;

        public string Icon { get; set; } = null!;
    }
}
