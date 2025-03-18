using SQLite;

namespace RestaurantPos.Data
{
    public class MenuItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Icon { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
