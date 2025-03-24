namespace RestaurantPos.Controls;

public partial class CurrentDateTimeControl : ContentView, IDisposable
{
	private readonly PeriodicTimer timer;
	public CurrentDateTimeControl()
	{
		InitializeComponent();
        String date = DateTime.Now.ToString("dddd, dd/MM/yyyy");
        dateLabel.Text = char.ToUpper(date[0]) + date.Substring(1);

        dayTimeLabel.Text = DateTime.Now.ToString("Текущо Време: HH:mm:ss");

        timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        UpdateTimer();
    }

    private async void UpdateTimer()
	{
		while (await timer.WaitForNextTickAsync())
		{
            // 3 seconds
            String date = DateTime.Now.ToString("dddd, dd/MM/yyyy");
            dateLabel.Text = char.ToUpper(date[0]) + date.Substring(1);
            dayTimeLabel.Text = DateTime.Now.ToString("Текущо Време: HH:mm:ss");
        }
	}

    public void Dispose() => timer.Dispose();
}