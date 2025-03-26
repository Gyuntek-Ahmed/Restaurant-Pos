using CommunityToolkit.Maui.Views;
using System.Threading.Tasks;

namespace RestaurantPos.Controls;

public partial class HelpPopup : Popup
{
	public const string Email = "gyuntekahmed@abv.bg";
	public const string Phone = "0893794549";

	public HelpPopup()
	{
		InitializeComponent();
	}

    private async void CloseLabel_Tapped(object sender, TappedEventArgs e)
		=> await this.CloseAsync();

    private async void Footer_Tapped(object sender, TappedEventArgs e)
		=> await Launcher.Default.OpenAsync("https://github.com/Gyuntek-Ahmed");
    
	private async void CopyEmail_Tapped(object sender, TappedEventArgs e)
	{
        await Clipboard.SetTextAsync(Email);
		copyEmailLable.Text = "Копирано!";
		await Task.Delay(2000);
        copyEmailLable.Text = "Копирай";
    }

    private async void CopyPhone_Tapped(object sender, TappedEventArgs e)
	{
        await Clipboard.SetTextAsync(Phone);
        copyPhoneLable.Text = "Копирано!";
        await Task.Delay(2000);
        copyPhoneLable.Text = "Копирай";
    }
}