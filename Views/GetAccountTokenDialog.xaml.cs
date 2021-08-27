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

using AnimeApp.ViewModels;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Views
{
    public sealed partial class GetAccountTokenDialog : ContentDialog
    {
        private AccountTokenViewModel vm;

        public GetAccountTokenDialog()
        {
            this.InitializeComponent();
            vm = new AccountTokenViewModel();

            vm.OpenBrowser();
        }

        private void AuthCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            LinkButton.IsEnabled = textBox.Text.Length >= 50;
        }

        private async void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            VerificationFailedTextBlock.Visibility = Visibility.Collapsed;
            VerificationStackPanel.Visibility = Visibility.Visible;

            bool isValid = await vm.VerifyToken(AuthCodeTextBox.Text);

            if (isValid)
                Hide();
            else
            {
                VerificationStackPanel.Visibility = Visibility.Collapsed;
                VerificationFailedTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
