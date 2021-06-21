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

        //Enables the Authorize button when character count is above 50.
        private void TokenBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            AuthorizeButton.IsEnabled = textBox.Text.Length >= 50;
        }

        //User has pasted the token into TextBox.
        //Verify if token is correct.
        private async void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            //Updates the UI to reflect the verification.
            FetchProfile.Visibility = Visibility.Visible;
            FetchFailed.Visibility = Visibility.Collapsed;

            //Token is not yet verified if valid.
            //Store it in a variable, if valid, then store it into static class, and save it on disk.
            //If invalid, show error UI.
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
