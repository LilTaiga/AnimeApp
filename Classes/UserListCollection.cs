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

        private UserList currentList;
        private UserList completedList;
        private UserList pausedList;
        private UserList droppedList;
        private UserList planningList;
        private UserList repeatingList;

        private List<UserList> customLists;

        public UserListCollection()
        {
            currentList = new UserList()
            {
                Name = "Watching",
                Status = EntryStatus.Current,
                IsCustomList = false,
                Length = 0,
                Entries = null
            };

            completedList = new UserList()
            {
                Name = "Completed",
                Status = EntryStatus.Completed,
                IsCustomList = false,
                Length = 0,
                Entries = null
            };

            pausedList = new UserList()
            {
                Name = "Paused",
                Status = EntryStatus.Paused,
                IsCustomList = false,
                Length = 0,
                Entries = null
            };

            droppedList = new UserList()
            {
                Name = "Dropped",
                Status = EntryStatus.Dropped,
                IsCustomList = false,
                Length = 0,
                Entries = null
            };

            planningList = new UserList()
            {
                Name = "Paused",
                Status = EntryStatus.Planning,
                IsCustomList = false,
                Length = 0,
                Entries = null
            };

            repeatingList = new UserList()
            {
                Name = "Rewatching",
                Status = EntryStatus.Repeating,
                IsCustomList = false,
                Length = 0,
                Entries = null
            };
        }

        public async Task DownloadUserLists()
        {
            if (userAccount == null)
                return;

            AnilistResponse response = await Anilist.AnilistOperations.GetViewerAnimeLists();
            if (response.data == null)
                throw new Exception("Error downloading data from Anilist");

            List currList = response.data.MediaListCollection.lists.Find(n => n.name == "Watching");
            List compList = response.data.MediaListCollection.lists.Find(n => n.name == "Completed");
            List pausList = response.data.MediaListCollection.lists.Find(n => n.name == "Paused");
            List dropList = response.data.MediaListCollection.lists.Find(n => n.name == "Dropped");
            List planList = response.data.MediaListCollection.lists.Find(n => n.name == "Planning");
            List repeList = response.data.MediaListCollection.lists.Find(n => n.name == "Rewatching");

            if (currList != null)
                ReplaceList(currentList, currList);
            if (compList != null)
                ReplaceList(currentList, currList);
            if (pausList != null)
                ReplaceList(pausedList, pausList);
            if (dropList != null)
                ReplaceList(droppedList, dropList);
            if (planList != null)
                ReplaceList(planningList, planList);
            if (repeList != null)
                ReplaceList(repeatingList, repeList);
        }

        private void ReplaceList(UserList _list, List _aniList)
        {

        }
    }
}
