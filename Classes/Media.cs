using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    public abstract class Media
    {
        int id;                             //The Anilist ID of the media

        MediaTitle titles;                  //All the titles and alternative titles of the media
        string description;                 //The description of the media

        MediaStatus status;                 //The current status of the media

        DateTime startDate;                 //The date when the media started releasing
        DateTime endDate;                   //The date when the media was finished

        int updated;                        //The last time when this media information was updated on Anilist

        MediaImage cover;                   //The image of the media's cover
        MediaImage banner;                  //The image of the media's banner in Anilist

        List<string> genres;                //A list of all genres of this media
        List<MediaTag> tags;                //A list of all tags of this media

        int averageScore = -1;              //The average score of this media in Anilist (-1 if unavaliable)
        int popularity = -1;                //The quantity of users that have this media in their lists (-1 if unavaliable)

        bool isAdult;                       //If this media is +18 only

        string siteUrl;                     //The link for the Anilist page of this media
    }
}
