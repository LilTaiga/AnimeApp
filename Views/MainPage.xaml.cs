using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using MUXC = Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            var titlebar = ApplicationView.GetForCurrentView().TitleBar;
            titlebar.ButtonBackgroundColor = Colors.Transparent;

            UpdateTitleBarLayout(coreTitleBar);
            Window.Current.SetTitleBar(TitleBarDraggableArea);
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            MainNavigationView.SelectedItem = MainNavigationView.MenuItems[1];
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            UpdateTitleBarLayout(sender);
        }

        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            // Get the size of the caption controls area and back button 
            // (returned in logical pixels), and move your content around as necessary.
            RightPaddingColumn.Width = new GridLength(coreTitleBar.SystemOverlayRightInset);
        }

        private void MainNavigationView_PaneOpening(MUXC.NavigationView sender, object args)
        {
            LeftPaddingColumn.Width = new GridLength(MainNavigationView.OpenPaneLength);
        }

        private void MainNavigationView_PaneClosing(MUXC.NavigationView sender, MUXC.NavigationViewPaneClosingEventArgs args)
        {
            LeftPaddingColumn.Width = new GridLength(MainNavigationView.CompactPaneLength);
        }

        //A new tab was selected in the main window.
        private void MainNavigationView_SelectionChanged(MUXC.NavigationView sender, MUXC.NavigationViewSelectionChangedEventArgs args)
        {
            //Gets the selected item object.
            //If the object is not an menu item, return. Something has gone very wrong.
            if (!(args.SelectedItem is MUXC.NavigationViewItem selectedItem))
                return;

            //The object has a tag (probably), so we use that to select the page to navigate to.
            switch (selectedItem.Tag)
            {
                case "Accounts":
                    MainFrame.Navigate(typeof(Views.Account));
                    break;
                default:
                    MainFrame.Navigate(typeof(Views.NotImplemented));
                    break;
            }
        }
    }
}
