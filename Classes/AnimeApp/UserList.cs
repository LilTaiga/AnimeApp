using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Classes;
using AnimeApp.Enums;

namespace AnimeApp.Classes.AnimeApp
{
    public class UserList
    {
        #region Attributes
        public string listName;
        public EntryStatus listStatus;
        public List<UserEntry> entries;
        #endregion



        //
        // The constructor
        //

        public UserList(Anilist.Result.List userList)
        {
            listName = userList.name;

            if (userList.isCustomList)
                listStatus = EntryStatus.Current;
            else if (!Enum.TryParse(userList.status, out listStatus))
                listStatus = EntryStatus.Unknown;

            if (userList.entries.Count == 0)
                return;

            entries = new List<UserEntry>();
            foreach(Anilist.Result.Entry entry in userList.entries)
            {
                var newEntry = new UserEntry(entry);
                newEntry.status = listStatus;

                entries.Add(newEntry);
            }
        }

    }
}
