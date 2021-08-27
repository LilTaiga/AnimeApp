using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Classes.Anilist;
using AnimeApp.Classes;

namespace AnimeApp.ViewModels
{
    class AccountTokenViewModel
    {
        public async Task<bool> VerifyToken(string _token)
        {
            try
            {
                var result = await Operations.GetViewer(_token);
                if (result.data != null)
                {
                    Account newacc = new Account(result.data.Viewer.id, _token);
                    UserAccount newAccount = AccountManager.CreateAccount(result.data.Viewer.name, newacc);

                    newAccount.ChangeAvatar(result.data.Viewer.avatar.large);
                    AccountManager.SetCurrentAccount(newAccount);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task OpenBrowser()
        {
            Uri uri = new Uri("https://anilist.co/login?apiVersion=v2&client_id=4444&response_type=token");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
