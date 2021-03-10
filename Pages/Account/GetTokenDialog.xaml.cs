using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using AnimeApp.Classes.Anilist;
using Windows.Storage;
using System.Text.Json;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages.Account
{
    public sealed partial class GetTokenDialog : ContentDialog
    {
        public GetTokenDialog()
        {
            this.InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            AuthorizeButton.IsEnabled = textBox.Text.Length >= 50;
        }

        private async void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            FetchProfile.Visibility = Visibility.Visible;
            FetchFailed.Visibility = Visibility.Collapsed;

            //Token is not yet verified if valid.
            //Store it in a variable, if variable, then store it into static class, and save it on disk.
            string token = TokenBox.Text;
            try
            {
                await AnilistAccount.Register(token);
            }
            catch
            {
                FetchProfile.Visibility = Visibility.Collapsed;
                FetchFailed.Visibility = Visibility.Visible;

                return;
            }
            
            Hide();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
