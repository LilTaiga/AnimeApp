using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Classes
{
    public static class ListManager
    {
        public static void DownloadUserLists(UserAccount _userAcc)
        {
            if (_userAcc.Anilist.Id == 0 || _userAcc.Anilist.AuthToken == null)
                throw new Exception("Error: No data to Anilist account avaliable.");


        }
    }
}
