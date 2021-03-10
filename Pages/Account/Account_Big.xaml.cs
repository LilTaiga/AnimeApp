﻿using System;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using AnimeApp.Classes.Anilist;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages.Account
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Account_Big : Page
    {
        public Account_Big()
        {
            this.InitializeComponent();
            LoadProfile();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            UserNotFoundText.Visibility = Visibility.Collapsed;
            UsernameProgressRing.IsActive = true;
            UsernameTextBox.IsEnabled = false;

            string username = UsernameTextBox.Text;

            var result = await AnilistQuery.SearchForUser(username);
            if(result.data.User != null)
            {
                Windows.System.Launcher.LaunchUriAsync(new Uri("https://anilist.co/api/v2/oauth/authorize?client_id=4444&response_type=token"));

                var getTokenDialog = new GetTokenDialog();
                await getTokenDialog.ShowAsync();
            }
            else
            {
                UserNotFoundText.Visibility = Visibility.Visible;
            }
            LoadProfile();

            UsernameProgressRing.IsActive = false;
            UsernameTextBox.IsEnabled = true;
        }

        private void LoadProfile()
        {
            if (AnilistAccount.Id == null)
            {
                LoggedPanel.Visibility = Visibility.Collapsed;
                LogInPanel.Visibility = Visibility.Visible;
                return;
            }

            LogInPanel.Visibility = Visibility.Collapsed;
            LoggedPanel.Visibility = Visibility.Visible;
            UsernameText.Text = AnilistAccount.Name;

            ProfileAvatar.ProfilePicture = new BitmapImage(new Uri(AnilistAccount.AvatarLarge));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoggedPanel.Visibility = Visibility.Collapsed;
            LogInPanel.Visibility = Visibility.Visible;
        }
    }
}