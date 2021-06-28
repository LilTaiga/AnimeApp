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

using muxc = Microsoft.UI.Xaml.Controls;

using AnimeApp.Classes.Anilist;
using AnimeApp.Classes.Anilist.Result;
using AnimeApp.Enums;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages
{
    public sealed partial class AnimeList : Page
    {
        private bool isOrderingCrescent;

        public List<Entry> groupEntries;                        //All entries from the current list.
        public List<Entry> groupEntriesSorted;                  //All entries from the current list, after being sorted.
        public List<Entry> groupEntriesFiltered;                //All entries from the current list, after being sorted, after being filtered.
        public ObservableCollection<Entry> visibleEntries;      //The displayed entries on the user screen.

        #region Page initialization

        //Initializes page and it's resources.
        public AnimeList()
        {
            InitializeComponent();

            groupEntries = new List<Entry>();
            groupEntriesSorted = new List<Entry>();
            groupEntriesFiltered = new List<Entry>();
            visibleEntries = new ObservableCollection<Entry>();

            SetupView();
        }

        //Select the default options on navigation bar, to not leave it without selections.
        private async void SetupView()
        {
            RetrievingFailedPanel.Visibility = Visibility.Collapsed;
            NotLoggedInPanel.Visibility = Visibility.Collapsed;

            RetrievingListsPanel.Visibility = Visibility.Visible;

            //Checks if user is logged, if not, display message to Log in.
            if (AnilistAccount.Token == null)
                NotLoggedIn();

            //User is logged in, but hasn't retrieved their lists yet.
            if (AnilistAccount.UserLists == null)
            {
                await DisplayRetrieving();
            }

            //User is logged in, and has retrieved their lists.
            if (AnilistAccount.UserLists != null)
            {
                RetrievingListsPanel.Visibility = Visibility.Collapsed;
                /*AnimeWatching.Content += " (" + AnilistAccount.UserLists[3].entries.Count + ")";
                AnimeCompleted.Content += " (" + AnilistAccount.UserLists[0].entries.Count + ")";
                AnimePaused.Content += " (" + AnilistAccount.UserLists[2].entries.Count + ")";
                AnimeDropped.Content += " (" + AnilistAccount.UserLists[4].entries.Count + ")";
                AnimePlanning.Content += " (" + AnilistAccount.UserLists[1].entries.Count + ")";*/
                ExhibitionModeListBig_Click(null, null);

                ChangeTab(MediaStatus.CURRENT);

                isOrderingCrescent = true;
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

        private void NavigationView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
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
            OrderComboBox.SelectedIndex = (int)sortBy - 1;

            switch (sortBy)
            {
                case SortColumn.Title:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.media.title.userPreferred).ToList();
                    break;
                case SortColumn.Score:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.score).ToList();
                    break;
                case SortColumn.Progress:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.progress).ToList();
                    break;
                case SortColumn.Last_Updated:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.updatedAt).ToList();
                    break;
                case SortColumn.Last_Added:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.createdAt).ToList();
                    break;
                case SortColumn.Start_Date:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.startedAt).ToList();
                    break;
                case SortColumn.Completed_Date:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.completedAt).ToList();
                    break;
                case SortColumn.Release_Date:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.media.startDate).ToList();
                    break;
                case SortColumn.Average_Score:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.media.averageScore ?? 0).ToList();
                    break;
                case SortColumn.Popularity:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.media.popularity).ToList();
                    break;
            }

            if (isOrderingCrescent)
                groupEntriesSorted.Reverse();

            UpdateEntriesFiltered();
        }

        private void UpdateEntriesSorted()
        {
            groupEntriesSorted.Clear();
            if (OrderComboBox.SelectedIndex != -1)
            {
                SortEntries((SortColumn)Enum.Parse(typeof(SortColumn), (((ComboBoxItem)OrderComboBox.SelectedItem).Tag ??
                                                                       ((ComboBoxItem)OrderComboBox.SelectedItem).Content).ToString()));
            }
            else
            {
                groupEntriesSorted.AddRange(groupEntries);
            }
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortEntries((SortColumn)Enum.Parse(typeof(SortColumn), (((ComboBoxItem)OrderComboBox.SelectedItem).Tag ??
                                                                   ((ComboBoxItem)OrderComboBox.SelectedItem).Content).ToString()));
            UpdateView();
        }


        private void OrderwayButton_Click(object sender, RoutedEventArgs e)
        {
            isOrderingCrescent = !isOrderingCrescent;
            groupEntriesFiltered.Reverse();
            UpdateView();

            if (OrderwayIcon.Glyph == "\uE70E")
                OrderwayIcon.Glyph = "\uE70D";
            else
                OrderwayIcon.Glyph = "\uE70E";
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
            foreach(muxc.NavigationViewItem item in NavView.MenuItems)
            {
                item.IsEnabled = false;
            }

            SearchBox.IsEnabled = false;
            OrderwayButton.IsEnabled = false;
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
            var parentNavView = ((RelativePanel)Frame.Parent).Parent as muxc.NavigationView;
            foreach(muxc.NavigationViewItem item in parentNavView.FooterMenuItems.OfType<muxc.NavigationViewItem>())
            {
                if ((item.Tag??"").ToString().Equals("Account"))
                    parentNavView.SelectedItem = item;
            }
        }

        #endregion

        private void ExhibitionModeGrid_Click(object sender, RoutedEventArgs e)
        {
            ExhibitionModeIcon.Glyph = "\uF0E2";

            UnloadObject(ExhibitionModeListBigPanel);
            UnloadObject(ExhibitionModeListCompactPanel);
            GC.Collect();

            FindName("ExhibitionModeGridPanel");
        }

        private void ExhibitionModeListBig_Click(object sender, RoutedEventArgs e)
        {
            ExhibitionModeIcon.Glyph = "\uE179";

            UnloadObject(ExhibitionModeGridPanel);
            UnloadObject(ExhibitionModeListCompactPanel);
            GC.Collect();

            FindName("ExhibitionModeListBigPanel");
        }

        private void ExhibitionModeListCompact_Click(object sender, RoutedEventArgs e)
        {
            ExhibitionModeIcon.Glyph = "\uE14C";

            UnloadObject(ExhibitionModeGridPanel);
            UnloadObject(ExhibitionModeListBigPanel);
            GC.Collect();

            FindName("ExhibitionModeListCompactPanel");
        }
    }
}