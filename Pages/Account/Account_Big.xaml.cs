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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using AnimeApp.Classes.Anilist;
using AnimeApp.Classes.Anilist.Result;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages.Account
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Account_Big : Page
    {
        //Page initialization
        //Checks if user is logged, to update UI.
        public Account_Big()
        {
            this.InitializeComponent();
            LoadProfile();
        }

        //User has typed in a username.
        //Tries to verify the authenticity of user.
        //Updates the UI accordingly, indicating success or failure
        private async void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            //Updates the UI
            UserNotFoundText.Visibility = Visibility.Collapsed;
            UsernameProgressRing.IsActive = true;
            UsernameTextBox.IsEnabled = false;


            bool success = await FetchProfile(UsernameTextBox.Text);

            if(success)
                LoadProfile();

            UsernameProgressRing.IsActive = false;
            UsernameTextBox.IsEnabled = true;
        }

        //Tries fetching the profile from Anilist.
        //Handles all exceptions that a AnilistQuery can cause.
        //If profile is found, then returns true to indicate success.
        //Otherwise, returns false to indicate failure (Not Found, Network Error, etc...)
        private async Task<bool> FetchProfile(string _username)
        {
            AnilistResponse result;

            try
            {
                result = await AnilistOperations.SearchForUser(_username);
            }
            catch(Exception e)
            {
                throw new Exception("Failed profile fetch.", e);
            }

            //The connection was successful.
            //Now we need to examinate the contents of the response.
            if (result.data.User != null)
            {
                //A profile was found.
                //Now lets check if this profile is the user's.
                await GetTokenFromUser();
                return true;
            }

            //The profile requested was not found.
            //Update the UI to indicate to reason of failure.
            UserNotFoundText.Visibility = Visibility.Visible;
            return false;
        }

        //Verifies the authenticity of user.
        private async Task GetTokenFromUser()
        {
            //Launches browser window.
            Windows.System.Launcher.LaunchUriAsync(new Uri("https://anilist.co/api/v2/oauth/authorize?client_id=4444&response_type=token"));

            //Show content dialog for user to input their token.
            var getTokenDialog = new GetTokenDialog();
            await getTokenDialog.ShowAsync();
        }

        //Tries to load a profile.
        private void LoadProfile()
        {
            //There is not profile to load.
            //Return to the LogIn panel.
            if (AnilistAccount.Id == null)
            {
                LoggedPanel.Visibility = Visibility.Collapsed;
                LogInPanel.Visibility = Visibility.Visible;
                return;
            }

            //Profile found.
            //Updates the UI to the logged on panel.
            //Also updates the profile picture to the user's avatar.
            LogInPanel.Visibility = Visibility.Collapsed;
            LoggedPanel.Visibility = Visibility.Visible;
            UsernameText.Text = AnilistAccount.Name;

            ProfileAvatar.ProfilePicture = new BitmapImage(new Uri(AnilistAccount.AvatarLarge));
        }

        //User clicked the "Log in with another profile" button.
        //Unloads and deletes all information about previous user.
        //Return to the LogIn panel.
        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            LoggedPanel.Visibility = Visibility.Collapsed;
            LogInPanel.Visibility = Visibility.Visible;

            AnilistAccount.Unregister();
        }
    }
}
