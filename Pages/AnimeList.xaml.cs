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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnimeList : Page
    {
        public List<Entry> userEntries;                   //A collection of all user entries in his Anilist Account.
        public ObservableCollection<Entry> medias;        //A collection used to display user entries in the app.

        public AnimeList()
        {
            this.InitializeComponent();

            userEntries = new List<Entry>();
            medias = new ObservableCollection<Entry>();

            GetUserLists();
        }

        private async void GetUserLists()
        {
            if (AnilistAccount.UserLists == null)
                await AnilistAccount.RetrieveLists();

            List<List> lists = AnilistAccount.UserLists;
            
            foreach(List _list in lists)
            {
                foreach(Entry _entry in _list.entries)
                {
                    userEntries.Add(_entry);
                    medias.Add(_entry);
                }
            }

        }
    }
}
