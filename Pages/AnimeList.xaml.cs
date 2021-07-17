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
using AnimeApp.Classes.AnimeApp;
using AnimeApp.Enums;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages
{
    public sealed partial class AnimeList : Page
    {
        private SortColumn sortColumn;
        private bool isSortCrescent;
        public string SearchString { get; set; }

        public List<UserEntry> groupEntries;                        //All entries from the current list.
        public List<UserEntry> groupEntriesSorted;                  //All entries from the current list, after being sorted.
        public List<UserEntry> groupEntriesFiltered;                //All entries from the current list, after being sorted, after being filtered.
        public ObservableCollection<UserEntry> visibleEntries;      //The displayed entries on the user screen.

        #region Page initialization

        //Initializes page and it's resources.
        public AnimeList()
        {
            InitializeComponent();

            groupEntries = new List<UserEntry>();
            groupEntriesSorted = new List<UserEntry>();
            groupEntriesFiltered = new List<UserEntry>();
            visibleEntries = new ObservableCollection<UserEntry>();

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
                /*AnimeWatching.Content += " (" + AnilistAccount.UserLists.Find(n => n.status == MediaStatus.CURRENT.ToString()).entries.Count + ")";
                AnimeCompleted.Content += " (" + AnilistAccount.UserLists.Find(n => n.status == MediaStatus.COMPLETED.ToString()).entries.Count + ")";
                AnimePaused.Content += " (" + AnilistAccount.UserLists.Find(n => n.status == MediaStatus.PAUSED.ToString()).entries.Count + ")";
                AnimeDropped.Content += " (" + AnilistAccount.UserLists.Find(n => n.status == MediaStatus.DROPPED.ToString()).entries.Count + ")";
                AnimePlanning.Content += " (" + AnilistAccount.UserLists.Find(n => n.status == MediaStatus.PLANNING.ToString()).entries.Count + ")";*/
                ExhibitionModeListBig_Click(null, null);

                NavView.SelectedItem = NavView.MenuItems[0];
                ChangeTab(EntryStatus.CURRENT);

                sortColumn = SortColumn.Progress;
                isSortCrescent = true;
                SortEntries();
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
            catch (Exception e)
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

        private void ChangeTab(EntryStatus _tab)
        {
            groupEntries.Clear();
            foreach (UserList _list in AnilistAccount.UserLists)
            {
                if (_list.listStatus.ToString().ToLower() == _tab.ToString().ToLower())
                {
                    groupEntries.AddRange(_list.entries);
                    break;
                }
            }
            UpdateEntriesSorted();
            UpdateEntriesFiltered();
        }

        private void NavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                var tabTag = args.SelectedItemContainer.Tag.ToString();

                ChangeTab((EntryStatus)Enum.Parse(typeof(EntryStatus), tabTag));
                UpdateView();
            }
        }

        #endregion



        #region NavigationView Sort

        private void SortEntries()
        {
            switch (sortColumn)
            {
                case SortColumn.Title:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.Media.title).ToList();
                    break;
                case SortColumn.Score:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.score).ToList();
                    break;
                case SortColumn.Progress:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.progress).ToList();
                    break;
                case SortColumn.Last_Updated:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.updated).ToList();
                    break;
                case SortColumn.Last_Added:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.created).ToList();
                    break;
                case SortColumn.Start_Date:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.started).ToList();
                    break;
                case SortColumn.Completed_Date:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.ended).ToList();
                    break;
                case SortColumn.Release_Date:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.Media.startDate).ToList();
                    break;
                case SortColumn.Average_Score:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.Media.averageScore).ToList();
                    break;
                case SortColumn.Popularity:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.Media.popularity).ToList();
                    break;
            }

            if (isSortCrescent)
                groupEntriesSorted.Reverse();

            UpdateEntriesFiltered();
        }

        private void UpdateEntriesSorted()
        {
            groupEntriesSorted.Clear();
            SortEntries();
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox bb = sender as ComboBox;
            sortColumn = (SortColumn)Enum.Parse(typeof(SortColumn), (((ComboBoxItem)bb.SelectedItem).Tag ??
                                                                    ((ComboBoxItem)bb.SelectedItem).Content).ToString());

            SortEntries();
            UpdateView();
        }


        private void OrderwayButton_Click(object sender, RoutedEventArgs e)
        {
            groupEntriesFiltered.Reverse();
            UpdateView();

            if (isSortCrescent)
            {
                OrderwayExpandedIcon.Glyph = "\uE70D";
                OrderwayCollapsedIcon.Glyph = "\uE70D";
            }
            else
            {
                OrderwayExpandedIcon.Glyph = "\uE70E";
                OrderwayCollapsedIcon.Glyph = "\uE70E";
            }

            isSortCrescent = !isSortCrescent;
        }

        #endregion



        #region NavigationView Search

        private void SearchEntries()
        {
            groupEntriesFiltered.Clear();

            var filtered = groupEntriesSorted.FindAll(groupEntriesSorted => groupEntriesSorted.Media.title.Contains(SearchString, StringComparison.CurrentCultureIgnoreCase));
            groupEntriesFiltered.AddRange(filtered);
        }

        private void UpdateEntriesFiltered()
        {
            groupEntriesFiltered.Clear();
            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                SearchEntries();
            }
            else
            {
                groupEntriesFiltered.AddRange(groupEntriesSorted);
            }
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            SearchString = sender.Text;
            SearchEntries();
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

            SearchBoxButton.IsEnabled = false;
            OrderwayButton.IsEnabled = false;

            SearchBoxExpanded.IsEnabled = false;
            OrderComboBoxExpanded.IsEnabled = false;
            OrderwayExpandedButton.IsEnabled = false;

            ExhibitionModeButton.IsEnabled = false;
        }

        //There are entries to navigate, display them in the screen.
        private void DisplayEntries()
        {
            visibleEntries.Clear();

            foreach (UserEntry _entry in groupEntriesFiltered)
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

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 1150 && e.PreviousSize.Width >= 1150)
                ExpandedToCompact.Begin();
            else if(e.NewSize.Width >= 1150 && e.PreviousSize.Width < 1150)
                CompactToExpanded.Begin();
        }

        private void EntriesViewGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var entryView = new EntryView.EntryDialog((UserEntry)e.ClickedItem);
            entryView.ShowAsync();
        }

        private void EntriesViewListBig_ItemClick(object sender, ItemClickEventArgs e)
        {
            var entryView = new EntryView.EntryDialog((UserEntry)e.ClickedItem);
            entryView.ShowAsync();
        }

        private void EntriesViewListCompact_ItemClick(object sender, ItemClickEventArgs e)
        {
            var entryView = new EntryView.EntryDialog((UserEntry)e.ClickedItem);
            entryView.ShowAsync();
        }
    }
}