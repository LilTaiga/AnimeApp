using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

using AnimeApp.Classes.Anilist.Result;

namespace AnimeApp.Classes.Anilist
{
    //Stores all information related to the current Anilist account.
    public static class AnilistAccount
    {
        public static string Id { get; private set; }
        public static string Name { get; private set; }
        public static string AvatarMedium { get; private set; }
        public static string AvatarLarge { get; private set; }
        public static string Token { get; private set; }
        public static List<List> UserLists { get; private set; }

        #region Profile

        //Tries to authenticate with Anilist by fetching user's profile.
        //Load user information into memory.
        //If user don't have a token, the profile fetch will fail.
        //If user is already authenticated, raises an exception.
        public static async Task LogIn()
        {
            if (Token != null)
                throw new Exception("User already logged in.");

            Token = await LoadFromDisk();

            AnilistResponse result = await AnilistOperations.GetViewer();
            if (result.data.Viewer == null)
                throw new Exception("User not authorized.");

            Id = result.data.Viewer.id.ToString();
            Name = result.data.Viewer.name;
            AvatarMedium = result.data.Viewer.avatar.medium;
            AvatarLarge = result.data.Viewer.avatar.large;
        }

        //Tries to verify the authenticity of token.
        //If token is invalid, a exception is raised.
        //If token is valid, then store user information and save it on disk.
        //If there's a user registered already, an exception is raised.
        public static async Task Register(string _token)
        {
            if(Token != null)
                throw new Exception("User already registered.");


            AnilistResponse result = await AnilistOperations.GetViewer(_token);
            if (result.data.Viewer == null)
                throw new Exception("User not authorized.");

            //User identity verified
            Id = result.data.Viewer.id.ToString();
            Name = result.data.Viewer.name;
            AvatarMedium = result.data.Viewer.avatar.medium;
            AvatarLarge = result.data.Viewer.avatar.large;
            Token = _token;

            await SaveOnDisk();
        }

        //Deletes all information about user, from memory and disk.
        public static async Task Unregister()
        {
            Task diskDelete = DeleteFromDisk();

            Id = null;
            Name = null;
            AvatarMedium = null;
            AvatarLarge = null;
            Token = null;
            UserLists = null;

            await diskDelete;
            GC.Collect();
        }

        #endregion



        #region Retrieving user information

        //Retrieves user's lists of anime lists.
        //There are five official Anilist lists, and user custom lists.
        //Must pass parameter as true to retrieve custom lists.
        //If user is not logged in, an exception is raised.
        public static async Task RetrieveLists(bool includeCustomLists = false)
        {
            if (Token == null)
                throw new Exception("User not logged in.");

            //Get user lists.
            AnilistResponse result = await AnilistOperations.GetViewerAnimeLists();
            List<List> lists = result.data.MediaListCollection.lists;

            //Remove custom lists.
            if(!includeCustomLists)
                lists.RemoveAll(RemoveCustomLists);

            //Store user lists.
            UserLists = lists;
        }

        //A LINQ function to remove custom lists.
        //Returns false to maintain official lists.
        //Return true to remove custom lists.
        private static bool RemoveCustomLists(Result.List _item)
        {
            switch(_item.status)
            {
                case "COMPLETED":
                case "PLANNING":
                case "CURRENT":
                case "PAUSED":
                case "DROPPED":
                    return false;
                default:
                    return true;
            }
        }

        #endregion



        #region Saving & Loading - Storage

        //Saves the current token into disk.
        //Replaces old token, if it exists.
        private static async Task SaveOnDisk()
        {
            StorageFolder cacheFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFile file = await cacheFolder.CreateFileAsync("Anilist", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, Token);
        }

        //Tries to load user's token from disk.
        //If token is found, returns it.
        //If token is not found, returns null.
        private static async Task<string> LoadFromDisk()
        {
            StorageFolder cacheFolder = ApplicationData.Current.LocalCacheFolder;

            try
            {
                StorageFile file = await cacheFolder.GetFileAsync("Anilist");
                return await FileIO.ReadTextAsync(file);
            }
            catch
            {
                return null;
            }
        }

        private static async Task DeleteFromDisk()
        {
            StorageFolder cacheFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFile file = await cacheFolder.GetFileAsync("Anilist");
            await file.DeleteAsync();
        }

        #endregion
    }
}