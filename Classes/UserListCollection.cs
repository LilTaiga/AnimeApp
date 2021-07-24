using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;
using AnimeApp.Classes.Anilist.Result;

namespace AnimeApp.Classes
{
    public class UserListCollection
    {
        private UserAccount userAccount;

        public UserList currentList;
        public UserList completedList;
        public UserList pausedList;
        public UserList droppedList;
        public UserList planningList;
        public UserList repeatingList;

        private List<UserList> customLists;

        public UserListCollection(UserAccount _acc)
        {
            userAccount = _acc;

            currentList = new UserList()
            {
                Name = "Watching",
                Status = EntryStatus.Current,
                IsCustomList = false,
                Length = 0,
                Entries = new List<Entry>()
            };

            completedList = new UserList()
            {
                Name = "Completed",
                Status = EntryStatus.Completed,
                IsCustomList = false,
                Length = 0,
                Entries = new List<Entry>()
        };

            pausedList = new UserList()
            {
                Name = "Paused",
                Status = EntryStatus.Paused,
                IsCustomList = false,
                Length = 0,
                Entries = new List<Entry>()
        };

            droppedList = new UserList()
            {
                Name = "Dropped",
                Status = EntryStatus.Dropped,
                IsCustomList = false,
                Length = 0,
                Entries = new List<Entry>()
            };

            planningList = new UserList()
            {
                Name = "Paused",
                Status = EntryStatus.Planning,
                IsCustomList = false,
                Length = 0,
                Entries = new List<Entry>()
            };

            repeatingList = new UserList()
            {
                Name = "Rewatching",
                Status = EntryStatus.Repeating,
                IsCustomList = false,
                Length = 0,
                Entries = new List<Entry>()
            };
        }

        public async Task DownloadUserLists()
        {
            if (userAccount == null)
                return;

            AnilistResponse response = await Anilist.AnilistOperations.GetUserAnimeLists(userAccount);
            if (response.data == null)
                throw new Exception("Error downloading data from Anilist");

            CollectDefaultLists(response.data.MediaListCollection);
        }

        private void CollectDefaultLists(MediaListCollection _lists)
        {
            List currList = _lists.lists.Find(n => n.name == "Watching");
            List compList = _lists.lists.Find(n => n.name == "Completed");
            List pausList = _lists.lists.Find(n => n.name == "Paused");
            List dropList = _lists.lists.Find(n => n.name == "Dropped");
            List planList = _lists.lists.Find(n => n.name == "Planning");
            List repeList = _lists.lists.Find(n => n.name == "Rewatching");

            if (currList != null)
                UpdateList(currentList, currList);
            if (compList != null)
                UpdateList(completedList, compList);
            if (pausList != null)
                UpdateList(pausedList, pausList);
            if (dropList != null)
                UpdateList(droppedList, dropList);
            if (planList != null)
                UpdateList(planningList, planList);
            if (repeList != null)
                UpdateList(repeatingList, repeList);
        }

        private void UpdateList(UserList _oldlist, List _newList)
        {
            //For each entry in the downloaded lists, find if it already exists in Animeapp's lists.
            //If there is, substitute the entry
            //If not, add a new entry
            foreach(Anilist.Result.Entry entry in _newList.entries)
            {
                Entry _oldEntry = _oldlist.Entries.Find(n => n.Id == entry.id);
                if (_oldEntry != null)
                    _oldEntry = new Entry(entry);
                else
                    _oldlist.Entries.Add(new Entry(entry));
            }
        }
    }
}
