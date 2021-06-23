using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using AnimeApp.Classes.Anilist.Result;
using AnimeApp.Enums;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages
{
    public sealed partial class AnimeList : Page
    {
        public List<Entry> groupEntries;                        //All entries from the current list.
        public List<Entry> groupEntriesSorted;                  //All entries from the current list, after being sorted.
        public List<Entry> groupEntriesFiltered;                //All entries from the current list, after being sorted, after being filtered.
        public ObservableCollection<Entry> visibleEntries;      //The displayed entries on the user screen.

        #region Page initialization

        //Initializes page and it's resources.
        public AnimeList()
        {
            this.InitializeComponent();

            groupEntries = new List<Entry>();
            groupEntriesSorted = new List<Entry>();
            groupEntriesFiltered = new List<Entry>();
            visibleEntries = new ObservableCollection<Entry>();

            SetupView();
        }

        //Select the default options on navigation bar, to not leave it without selections.
        private async void SetupView()
        {
            EntriesListView.Visibility = Visibility.Collapsed;
            RetrievingFailedPanel.Visibility = Visibility.Collapsed;
            NotLoggedInPanel.Visibility = Visibility.Collapsed;

            RetrievingListsPanel.Visibility = Visibility.Visible;

            //Checks if user is logged, if not, display message to Log in.
            if (AnilistAccount.Token == null)
                NotLoggedIn();

            //User is logged in, but hasn't retrieved his lists yet.
            if(AnilistAccount.UserLists == null)
            {
                await DisplayRetrieving();
            }

            if (AnilistAccount.UserLists != null)
            {
                RetrievingListsPanel.Visibility = Visibility.Collapsed;
                EntriesListView.Visibility = Visibility.Visible;

                ChangeTab(MediaStatus.CURRENT);
                SortEntries(SortColumn.Progress);
            }


            UpdateView();
        }

        //Display the "Retrieving your lists..." message.
        //
        private async Task DisplayRetrieving()
        {
            RetrievingListsPanel.Visibility = Visibility.Visible;

            try
            {
                await AnilistAccount.RetrieveLists();
            }
            catch
            {
                RetrievingFailed();
            }
        }

        private void RetrievingFailed()
        {
            RetrievingListsPanel.Visibility = Visibility.Collapsed;
            RetrievingFailedPanel.Visibility = Visibility.Visible;
        }

        private void NotLoggedIn()
        {
            RetrievingListsPanel.Visibility = Visibility.Collapsed;
            NotLoggedInPanel.Visibility = Visibility.Visible;
        }

        #endregion



        #region NavigationView Navigation

        private void ChangeTab(MediaStatus _tab)
        {
            groupEntries.Clear();
            foreach (List _list in AnilistAccount.UserLists)
            {
                if (_list.status == _tab.ToString())
                {
                    groupEntries.AddRange(_list.entries);
                    break;
                }
            }

            UpdateEntriesSorted();
            UpdateEntriesFiltered();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                var tabTag = args.SelectedItemContainer.Tag.ToString();

                ChangeTab((MediaStatus)Enum.Parse(typeof(MediaStatus), tabTag));
                UpdateView();
            }
        }

        #endregion



        #region NavigationView Sort

        private void SortEntries(SortColumn sortBy)
        {
            switch(sortBy)
            {
                case SortColumn.Title:
                    groupEntriesSorted = groupEntries.OrderBy(groupEntries => groupEntries.media.title.userPreferred).ToList();
                    break;
                case SortColumn.Score:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.score).ToList();
                    break;
                case SortColumn.Progress:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.progress).ToList();
                    break;
                default:
                    throw new Exception("Ops, you found something that you shouldn't have.");
            }

            UpdateEntriesFiltered();
        }

        private void UpdateEntriesSorted()
        {
            groupEntriesSorted.Clear();
            if (OrderComboBox.SelectedIndex != -1)
            {
                SortEntries((SortColumn)Enum.Parse(typeof(SortColumn), OrderComboBox.SelectedItem.ToString()));
            }
            else
            {
                groupEntriesSorted.AddRange(groupEntries);
            }
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortEntries((SortColumn)Enum.Parse(typeof(SortColumn), OrderComboBox.SelectedItem.ToString()));
            UpdateView();
        }

        #endregion



        #region NavigationView Search

        private void SearchEntries(string _searchText)
        {
            groupEntriesFiltered.Clear();

            var filtered = groupEntriesSorted.FindAll(groupEntriesSorted => groupEntriesSorted.media.title.romaji.Contains(_searchText, StringComparison.CurrentCultureIgnoreCase));
            groupEntriesFiltered.AddRange(filtered);
        }

        private void UpdateEntriesFiltered()
        {
            groupEntriesFiltered.Clear();
            if (!string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchEntries(SearchBox.Text);
            }
            else
            {
                groupEntriesFiltered.AddRange(groupEntriesSorted);
            }
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            SearchEntries(sender.Text);
            UpdateView();
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        #endregion



        #region View

        //Updates the screen, showing new entries or displaying an error screen.
        private void UpdateView()
        {
            if (AnilistAccount.UserLists == null)
                BlockNavigation();
            else
                DisplayEntries();
        }

        //No entries to navigate, block the UI to avoid unnecessary navigation.
        private void BlockNavigation()
        {
            foreach(NavigationViewItem item in NavView.MenuItems)
            {
                item.IsEnabled = false;
            }

            SearchBox.IsEnabled = false;
            OrderComboBox.IsEnabled = false;
        }

        //There are entries to navigate, display them in the screen.
        private void DisplayEntries()
        {
            visibleEntries.Clear();

            foreach (Entry _entry in groupEntriesFiltered)
                visibleEntries.Add(_entry);
        }

        //User clicked the button to go to Accounts page, redirect main NavigationView to Accounts page.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parentNavView = ((RelativePanel)Frame.Parent).Parent as Microsoft.UI.Xaml.Controls.NavigationView;
            foreach(Microsoft.UI.Xaml.Controls.NavigationViewItem item in parentNavView.FooterMenuItems.OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>())
            {
                if ((item.Tag??"").ToString().Equals("Account"))
                    parentNavView.SelectedItem = item;
            }

            //Frame.Navigate(typeof(AnimeApp.Pages.Account.Account_Big));
        }

        #endregion
    }
}