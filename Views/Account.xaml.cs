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
using AnimeApp.ViewModels;
using AnimeApp.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Account : Page
    {
        AccountViewModel vm = new AccountViewModel();

        public Account()
        {
            this.InitializeComponent();
        }

        private void TryToLogin()
        {

        }

        private async void LinkAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AccountNotFoundText.Visibility = Visibility.Collapsed;
            AccountLookupProgressRing.Visibility = Visibility.Visible;
            AccountLookUpText.Visibility = Visibility.Visible;

            var nameExists = await vm.VerifyUsername(AccountNameTextBox.Text);
            AccountLookupProgressRing.Visibility = Visibility.Collapsed;
            AccountLookUpText.Visibility = Visibility.Collapsed;

            if (nameExists)
            {
                GetAccountTokenDialog contentDialog = new GetAccountTokenDialog();
                await contentDialog.ShowAsync();
            }
            else
            {
                AccountNotFoundText.Visibility = Visibility.Visible;
            }
        }

        private void AccountNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            LinkAccountButton.IsEnabled = textBox.Text.Length >= 2;
        }
    }
}
