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
        public int Id { get; set; }                             //The Anilist ID of the media

        public MediaTitle Titles { get; set; }                  //All the titles and alternative titles of the media
        public string Description { get; set; }                 //The description of the media

        public MediaStatus Status { get; set; }                 //The current status of the media

        public DateTime StartDate { get; set; }                 //The date when the media started releasing
        public DateTime EndDate { get; set; }                   //The date when the media was finished

        public int Updated { get; set; }                        //The last time when this media information was updated on Anilist

        public MediaImage Cover { get; set; }                   //The image of the media's cover
        public MediaImage Banner { get; set; }                  //The image of the media's banner in Anilist

        public List<string> Genres { get; set; }                //A list of all genres of this media
        public List<MediaTag> Tags { get; set; }                //A list of all tags of this media

        public int AverageScore { get; set; }                   //The average score of this media in Anilist (-1 if unavaliable)
        public int Popularity { get; set; }                     //The quantity of users that have this media in their lists (-1 if unavaliable)

        public bool IsAdult { get; set; }                       //If this media is +18 only

        public string SiteUrl { get; set; }                     //The link for the Anilist page of this media
    }
}
