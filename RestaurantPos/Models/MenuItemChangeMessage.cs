using CommunityToolkit.Mvvm.Messaging.Messages;

namespace RestaurantPos.Models
{
    public class MenuItemChangeMessage : ValueChangedMessage<MenuItemModel>
    {
        public MenuItemChangeMessage(MenuItemModel value) : base(value)
        {
        }

        public static MenuItemChangeMessage From(MenuItemModel value) => new (value);
    }
}
