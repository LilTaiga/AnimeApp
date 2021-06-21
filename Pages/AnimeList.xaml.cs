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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnimeList : Page
    {
        public List<List> userLists;

        public List<Entry> visibleMedias;
        public ObservableCollection<Entry> medias;        //A collection used to display user entries in the app.

        private MediaStatus currentTab;

        public AnimeList()
        {
            this.InitializeComponent();

            visibleMedias = new List<Entry>();
            medias = new ObservableCollection<Entry>();

            GetUserLists();
        }

        private async void GetUserLists()
        {
            if (AnilistAccount.UserLists == null)
                await AnilistAccount.RetrieveLists();

            List<List> lists = AnilistAccount.UserLists;
            userLists = lists;
            
            foreach(List _list in lists)
            {
                foreach(Entry _entry in _list.entries)
                {
                    visibleMedias.Add(_entry);
                    medias.Add(_entry);
                }
            }

            NavView.SelectedItem = NavView.MenuItems[0];
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer != null)
            {
                var tabTag = args.SelectedItemContainer.Tag.ToString();

                if (tabTag != currentTab.ToString())
                {
                    NavigationView_ChangeTab((MediaStatus)Enum.Parse(typeof(MediaStatus), tabTag));
                    currentTab = (MediaStatus)Enum.Parse(typeof(MediaStatus), tabTag);
                }
            }
        }

        private void NavigationView_ChangeTab(MediaStatus _tab)
        {
            visibleMedias.Clear();
            medias.Clear();

            foreach(List _list in userLists)
            {
                if (_list.status == _tab.ToString())
                {
                    visibleMedias.AddRange(_list.entries);
                    break;
                }
            }

            if(OrderComboBox.SelectedIndex == -1)
            {
                foreach (Entry _entry in visibleMedias)
                {
                    medias.Add(_entry);
                }
            }
            else
            {
                SortVisibleMedias((SortColumn)Enum.Parse(typeof(SortColumn), OrderComboBox.SelectedItem.ToString()));
            }
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortVisibleMedias((SortColumn)Enum.Parse(typeof(SortColumn), e.AddedItems[0].ToString()));
        }

        private void ReorderViewList(IEnumerable<Entry> _newOrder)
        {
            visibleMedias.Clear();
            medias.Clear();

            visibleMedias.AddRange(_newOrder);

            foreach (Entry _entry in visibleMedias)
            {
                medias.Add(_entry);
            }
        }

        private void SortVisibleMedias(SortColumn sortBy)
        {
            IEnumerable<Entry> newOrder;

            if (sortBy == SortColumn.Title)
            {
                newOrder = visibleMedias.OrderBy(visibleMedias => visibleMedias.media.title).ToList();
                ReorderViewList(newOrder);
                return;
            }
            else if (sortBy == SortColumn.Progress)
            {
                newOrder = visibleMedias.OrderByDescending(visibleMedias => visibleMedias.progress).ToList();
                ReorderViewList(newOrder);
                return;
            }
            else if (sortBy == SortColumn.Score)
            {
                newOrder = visibleMedias.OrderByDescending(visibleMedias => visibleMedias.score).ToList();
                ReorderViewList(newOrder);
                return;
            }
        }
    }
}