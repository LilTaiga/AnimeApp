using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AnimeApp.Classes.Anilist
{
    public static class AnilistAccount
    {
        public static string Id { get; private set; }
        public static string Name { get; private set; }
        public static string AvatarMedium { get; private set; }
        public static string AvatarLarge { get; private set; }
        public static string Token { get; private set; }
        public static List<Result.List> UserLists { get; private set; }

        public static async Task LogIn()
        {
            if (Token == null)
                Token = await LoadFromDisk();

            var result = await AnilistQuery.GetViewer(Token);
            if (result.data.Viewer == null)
                throw new Exception("User not authorized.");

            Id = result.data.Viewer.id.ToString();
            Name = result.data.Viewer.name;
            AvatarMedium = result.data.Viewer.avatar.medium;
            AvatarLarge = result.data.Viewer.avatar.large;
        }

        public static async Task Register(string _token)
        {
            var result = await AnilistQuery.GetViewer(_token);
            if (result.data.Viewer == null)
                throw new Exception("User not authorized.");

            Id = result.data.Viewer.id.ToString();
            Name = result.data.Viewer.name;
            AvatarMedium = result.data.Viewer.avatar.medium;
            AvatarLarge = result.data.Viewer.avatar.large;
            Token = _token;

            await SaveOnDisk();
        }

        public static async Task RetrieveLists()
        {
            var result = await AnilistQuery.GetViewerDetails();
            var lists = result.data.MediaListCollection.lists;

            lists.RemoveAll(FilterLists);
            UserLists = lists;
        }

        private static bool FilterLists(Result.List _item)
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

        private static async Task SaveOnDisk()
        {
            var cacheFolder = ApplicationData.Current.LocalCacheFolder;
            var file = await cacheFolder.CreateFileAsync("Anilist", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, Token);
        }

        private static async Task<string> LoadFromDisk()
        {
            var cacheFolder = ApplicationData.Current.LocalCacheFolder;
            var file = await cacheFolder.GetFileAsync("Anilist");

            return await FileIO.ReadTextAsync(file);
        }
    }
}
