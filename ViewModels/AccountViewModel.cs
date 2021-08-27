using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Classes.Anilist;
using AnimeApp.Classes;

namespace AnimeApp.ViewModels
{
    public class AccountViewModel
    {
        public async Task<bool> VerifyUsername(string _username)
        {
            try
            {
                await Operations.SearchForUser(_username);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
