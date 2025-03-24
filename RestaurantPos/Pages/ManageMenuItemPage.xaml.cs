using RestaurantPos.ViewModels;
using System.Threading.Tasks;

namespace RestaurantPos.Pages;

public partial class ManageMenuItemPage : ContentPage
{
    private readonly ManageMenuItemsViewModel manageMenuItemsViewModel;

    public ManageMenuItemPage(ManageMenuItemsViewModel manageMenuItemsViewModel)
    {
		InitializeComponent();
        this.manageMenuItemsViewModel = manageMenuItemsViewModel;
        BindingContext = manageMenuItemsViewModel;
        InitializeAsync();
    }

    private async void InitializeAsync()
        => await manageMenuItemsViewModel.InitializeAsync();
    

    private async void MenuItemsListControl_OnSelectItem(Data.MenuItem menuItem)
        => await manageMenuItemsViewModel.EditMenuItemCommand.ExecuteAsync(menuItem);

    private async void CategoriesListControl_OnCategorySelected(Models.MenuCategoryModel category)
        => await manageMenuItemsViewModel.SelectCategoryCommand.ExecuteAsync(category);

    private void SaveMenuItemFormControl_OnCancel()
        => manageMenuItemsViewModel.CancelCommand.Execute(null);

    private async void SaveMenuItemFormControl_OnSaveItem(Models.MenuItemModel menuItemModel)
        => await manageMenuItemsViewModel.SaveMenuItemCommand.ExecuteAsync(menuItemModel);
}