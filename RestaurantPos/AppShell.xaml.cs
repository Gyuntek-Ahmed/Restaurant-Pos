﻿using CommunityToolkit.Maui.Views;
using RestaurantPos.Controls;

namespace RestaurantPos
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var helpPopup = new HelpPopup();
            await this.ShowPopupAsync(helpPopup);
        }
    }
}
