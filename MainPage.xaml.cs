using System;

using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

using AnimeApp.Classes.Anilist;
using Windows.UI.ViewManagement;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimeApp
{
    public sealed partial class MainPage : Page
    {
        //App initialization
        public MainPage()
        {
            AnilistAccount.LogIn();
            this.InitializeComponent();

            //Hides the default taskbar to set custom taskbar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            // Set XAML element as a draggable region.
            Window.Current.SetTitleBar(TitleBarArea);

            //Sets the taskbar buttons to transparent.
            var titlebar = ApplicationView.GetForCurrentView().TitleBar;
            titlebar.ButtonBackgroundColor = Colors.Transparent;
            titlebar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        //Called when the scaling or DPI changes.
        //Updates the taskbar height to be more accurate with the visual.
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            TitleBarArea.Height = sender.Height;
        }

        private void NavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem is Microsoft.UI.Xaml.Controls.NavigationViewItem selected)
            {
                switch (selected.Tag)
                {
                    case "Account":
                        NavViewFrame.Navigate(typeof(Pages.Account.Account_Big));
                        break;
                    case "AnimeList":
                        NavViewFrame.Navigate(typeof(Pages.AnimeList), this);
                        break;
                    default:
                        NavViewFrame.Navigate(typeof(Pages.NotImplemented));
                        break;
                }
            }
        }
    }
}
