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
        public List<List> userLists;                            //A reference to the current user lists, for ease of acess.

        public List<Entry> groupEntries;                        //All entries from the current list.
        public List<Entry> groupEntriesSorted;                  //All entries from the current list, after being sorted.
        public List<Entry> groupEntriesFiltered;                //All entries from the current list, after being sorted, after being filtered.
        public ObservableCollection<Entry> visibleEntries;      //The displayed entries on the user screen.

        //Default constructor
        //Initializes all lists to an empty list.
        public AnimeList()
        {
            this.InitializeComponent();

            groupEntries = new List<Entry>();
            groupEntriesSorted = new List<Entry>();
            groupEntriesFiltered = new List<Entry>();
            visibleEntries = new ObservableCollection<Entry>();

            SetupEntries();
        }

        private async void SetupEntries()
        {
            await GetUserLists();

            ChangeTab(MediaStatus.CURRENT);
            SortMedias(SortColumn.Progress);
            UpdateView();
        }

        private async Task GetUserLists()
        {
            if (AnilistAccount.UserLists == null)
                await AnilistAccount.RetrieveLists();

            List<List> lists = AnilistAccount.UserLists;
            userLists = lists;
        }

        private void ChangeTab(MediaStatus _tab)
        {
            groupEntries.Clear();
            groupEntriesSorted.Clear();
            groupEntriesFiltered.Clear();

            foreach(List _list in userLists)
            {
                if (_list.status == _tab.ToString())
                {
                    groupEntries.AddRange(_list.entries);
                    break;
                }
            }
        }

        private void SortMedias(SortColumn sortBy)
        {
            switch(sortBy)
            {
                case SortColumn.Title:
                    groupEntriesSorted = groupEntries.OrderBy(groupEntries => groupEntries.media.title.userPreferred).ToList();
                    return;
                case SortColumn.Score:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.score).ToList();
                    return;
                case SortColumn.Progress:
                    groupEntriesSorted = groupEntries.OrderByDescending(groupEntries => groupEntries.progress).ToList();
                    return;
                default:
                    throw new Exception("Ops, you found something that you shouldn't have.");
            }
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                var tabTag = args.SelectedItemContainer.Tag.ToString();

                ChangeTab((MediaStatus)Enum.Parse(typeof(MediaStatus), tabTag));
                if (OrderComboBox.SelectedIndex != -1)
                    SortMedias((SortColumn)Enum.Parse(typeof(SortColumn), OrderComboBox.SelectedItem.ToString()));
                else
                    groupEntriesSorted.AddRange(groupEntries);

                UpdateView();
            }
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortMedias((SortColumn)Enum.Parse(typeof(SortColumn), e.AddedItems[0].ToString()));
            UpdateView();
        }

        private void UpdateView()
        {
            visibleEntries.Clear();

            foreach(Entry _entry in groupEntriesSorted)
            {
                visibleEntries.Add(_entry);
            }
        }
    }
}