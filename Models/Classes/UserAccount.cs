using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;
using Windows.Storage;

namespace AnimeApp.Classes
{
    //An object that represents the current Animeapp account.
    //Store some relevant user information, along with user's settings.
    //Technically, Animeapp doesn't need an Anilist account to work
    //but that goes agaisn't the purpose of the app.
    public class UserAccount
    {

        #region Properties

        public string Name { get; set; }
        public UserAvatar Avatar { get; set; }

        public TitleLanguage TitlePreference { get; set; }

        public bool ViewAdultContent { get; set; }

        //Stores relevant information about the user's Anilist account.
        public Anilist.Account Anilist { get; set; }

        #endregion



        #region Constructor

        //The JsonSerializer requires a parameterless constructor to be able to desserialize an object.
        //Sadly, it must be public :(
        public UserAccount() { }

        //
        // The constructor
        // You can only create a Animeapp account linking your existing Anilist account
        // May change in the future, idk
        //

        internal UserAccount(string _name, Anilist.Account _aniAcc)
        {
            Name = _name;

            Anilist = _aniAcc;
        }

        #endregion



        #region Methods - ChangeUserInformation

        public async Task ChangeAvatar(string _newAvatarUrl)
        {
            try
            {
                await Utilities.HttpConnection.DownloadImageAsync(Name, new Uri(_newAvatarUrl));
            }
            catch (Exception e)
            {
                throw new Exception("Error downloading avatar", e);
            }

            if(Avatar == null)
                Avatar = new UserAvatar();

            Avatar.WebUrlPath = _newAvatarUrl;
            Avatar.ImagePath = ApplicationData.Current.LocalCacheFolder.Path + @"\images\" + Name + ".png";
        }

        public void ChangeTitlePreference(TitleLanguage _newPreference)
        {
            TitlePreference = _newPreference;
        }

        public void DisplayAdultContent(bool _viewContent)
        {
            ViewAdultContent = _viewContent;
        }

        #endregion
    }
}
