using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantPos.Data;

namespace RestaurantPos.Models
{
    public partial class MenuCategoryModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;

        [ObservableProperty]
        public bool isSelected;

        public static MenuCategoryModel FromEntity(MenuCategory entity) =>
        new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Icon = entity.Icon
        };
    }
}
