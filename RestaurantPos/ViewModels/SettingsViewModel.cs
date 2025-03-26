using CommunityToolkit.Mvvm.Messaging;
using RestaurantPos.Models;

namespace RestaurantPos.ViewModels
{
    public class SettingsViewModel
    {
        private const string NameKey = "name";
        private const string TaxPercentageKey = "tax";
        private bool isInitialized;

        public async ValueTask InitializeAsync()
        {
            if (isInitialized)
                return;


            isInitialized = true;

            var name = Preferences.Default.Get<string?>(NameKey, null);

            if (name is null)
            {
                do
                {
                    name = await Shell.Current.DisplayPromptAsync("Вашето Име", "Въведете вашето име");
                } while (string.IsNullOrWhiteSpace(name));

                Preferences.Default.Set<string>(NameKey, name);
            }

            WeakReferenceMessenger.Default.Send(NameChangedMessage.From(name));
        }

        public int GetTaxPercentage() => Preferences.Default.Get<int>(TaxPercentageKey, 0);
        public void SetTaxPercentage(int taxPercentage) => Preferences.Default.Set<int>(TaxPercentageKey, taxPercentage);
        
    }
}
