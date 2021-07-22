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
        AnimeFormat format;                 //The format of this anime

        int episodes = -1;                  //The number of episodes this anime has (-1 if unavaliable)
        int duration = -1;                  //The duration of each episode in minutes (-1 if unavaliable)

        string animationStudio;             //The name of the main studio that produced this anime
        List<string> producers;             //The name of all other studios that helped create this anime

        List<MediaAiring> nextAirings;      //A list of when the next episodes will air (up to 25 items)
    }
}
