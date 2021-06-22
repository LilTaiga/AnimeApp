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
            try
            {
                await GetUserLists();
            }
            catch
            {
                return;
            }

            ChangeTab(MediaStatus.CURRENT);
            SortEntries(SortColumn.Progress);
            UpdateView();
        }

        //Tries to get user lists.
        //Shows an "User not logged in" screen if it catches an exception.
        private async Task GetUserLists()
        {
            try
            {
                await AnilistAccount.RetrieveLists();
            }
            catch(Exception e)
            {
                //User not logged in.
                //TODO: Implement "User not logged in" screen.
                throw e;
            }
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

        private void UpdateView()
        {
            visibleEntries.Clear();

            foreach (Entry _entry in groupEntriesFiltered)
                visibleEntries.Add(_entry);
        }

        #endregion
    }
}