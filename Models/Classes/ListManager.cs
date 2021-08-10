using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Classes
{
    public static class ListManager
    {
        public static async Task<UserListCollection> DownloadUserLists(UserAccount _userAcc)
        {
            if (_userAcc.Anilist.Id == 0 || _userAcc.Anilist.AuthToken == null)
                throw new Exception("Error: No data to Anilist account avaliable.");

            UserListCollection lists = new UserListCollection(_userAcc);
            await lists.DownloadUserLists();

            return lists;
        }

        public static async Task SaveListsToDisk(UserListCollection _lists)
        {
            string name = _lists.UserAccount.Name;

            var currentTask = SaveListToDisk(name, _lists.currentList);
            var completedTask = SaveListToDisk(name, _lists.completedList);
            var pausedTask = SaveListToDisk(name, _lists.pausedList);
            var droppedTask = SaveListToDisk(name, _lists.droppedList);
            var planningTask = SaveListToDisk(name, _lists.planningList);
            var repeatingTask = SaveListToDisk(name, _lists.repeatingList);

            await Task.WhenAll(currentTask, completedTask, pausedTask, droppedTask, planningTask, repeatingTask);
        }

        public static async Task SaveListToDisk(string _userName, UserList _userList)
        {
            var json = Utilities.JsonHandler.FromObjectToJson(_userList);

            string filepath = _userName + "\\" + _userList.Name.ToLower() + ".json";
            await Utilities.FileHandler.WriteStringToLocalCacheFolder(filepath, json);
        }

        public static async Task<UserList> LoadListFromDisk(string _userName, string _listName)
        {
            string filepath = _userName + "\\" + _listName.ToLower() + ".json";

            var json = await Utilities.FileHandler.ReadStringFromLocalCacheFolder(filepath);
            return Utilities.JsonHandler.FromJsonToObject<UserList>(json);
        }
    }
}
