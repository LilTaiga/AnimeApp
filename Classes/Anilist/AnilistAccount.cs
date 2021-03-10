using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Classes.Anilist
{
    public class AnilistAccount
    {
        private static AnilistAccount instance;
        public static AnilistAccount GetInstance()
        {
            if (instance == null)
                instance = new AnilistAccount();


            return instance;
        }

        public string id;
        public string name;
        public string avatarMedium;
        public string avatarLarge;
        public string token;
        public AnilistResult.MediaListCollection lists;
    }
}
