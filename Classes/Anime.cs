using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    public class Anime : Media
    {
        public AnimeFormat Format { get; set; }               //The format of this anime

        public int Episodes { get; set; }                //The number of episodes this anime has (-1 if unavaliable)
        public int Duration { get; set; }                  //The duration of each episode in minutes (-1 if unavaliable)

        public string AnimationStudio { get; set; }            //The name of the main studio that produced this anime
        public List<string> Producers { get; set; }            //The name of all other studios that helped create this anime

        public List<MediaAiring> NextEpisodes { get; set; }      //A list of when the next episodes will air (up to 25 items)
    }
}
