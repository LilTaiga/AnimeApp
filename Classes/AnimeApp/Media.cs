using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes.AnimeApp
{
    public class Media
    {
        #region Attributes
        public int id;                                                  //The Anilist ID of the media

        public string title;                                            //The user prefered title of the media
        public string format;                                           //The format of this media
        public MediaStatus status;                                      //The status of the media (See enum)
        public string description;                                      //The markdown description of the media

        public DateTime startDate;                                      //The date which media started airing (if avaliable)
        public DateTime endDate;                                        //The date which media finished airing (if avaliable)
        public string seasonName;                                       //The season name (if avaliable)
        public int seasonYear;                                          //The season year (if not avaliable, set to -1)

        public int episodes;                                            //The quantity of episodes (if not avaliable, set to -1)
        public int duration;                                            //The duration of each episode (if not avaliable, set to -1)

        public string coverMedium;                                      //The link for the medium sized cover
        public string coverLarge;                                       //The link for the large sized cover
        public string coverExtraLarge;                                  //The link for the extra large sized cover

        public List<string> genres;                                     //The list of genres of this media
        public List<string> synonyms;                                   //The list of synonyms of this media

        public int averageScore;                                        //The average score of all users on Anilist (if not avaliable, set to -1)
        public int meanScore;                                           //The mean score of all users on Anilist (if not avaliable, set to -1)
        public int popularity;                                          //How many users have this media in their lists

        public Dictionary<string, int> tags;                            //List of tags of media
        public bool isAdult;                                            //This media is +18 and should probably be hiiden

        public DateTime nextAiringEpisode;                              //The date of next episode (if avaliable)
        public int nextEpisodeNumber;                                   //The number of the next episode (if not avaliable, set to -1)

        public string siteUrl;                                          //Link to the media official site (if avaliable)
        public Dictionary<string, string> otherLinks;             //Links related to the media (if avaliable)
        #endregion



        #region Methods
        //Returns the number of episodes the media has
        //If media has unknown number of episodes, returns the total number of aired episodes.
        //Also returns a bool to check if the number of episodes is known or not.
        public (int, bool) GetEpisodes()
        {
            if (episodes != -1)
                return (episodes, true);

            if (nextAiringEpisode > DateTimeOffset.FromUnixTimeSeconds(0).DateTime && nextEpisodeNumber != -1)
                return (nextEpisodeNumber - 1, false);

            return (-1, false);
        }

        public double GetEpisodesFormated()
        {
            if (episodes != -1)
                return episodes;

            if (nextAiringEpisode > DateTimeOffset.FromUnixTimeSeconds(0).DateTime && nextEpisodeNumber != -1)
                return nextEpisodeNumber - 1 != 0 ? nextEpisodeNumber - 1 : 1;

            return 1;
        }

        public string GetMediaFormatFormatted()
        {
            string newFormat = format.Replace('_', ' ').ToLower().Replace("tv", "TV").Replace("ova", "OVA").Replace("ona", "ONA");
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newFormat);
        }

        public string GetSeasonAndYear()
        {
            string season = seasonName ?? "Unknown";

            if (seasonYear != -1)
                season += " " + seasonYear.ToString();
            else if (startDate != null)
                season += " " + startDate.Year.ToString();

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(season.ToLower());
        }
        #endregion



        //
        // The Constructor
        //

        public Media(Anilist.Result.Media media)
        {
            id = media.id;

            title = media.title.userPreferred;
            format = media.format;
            if (!Enum.TryParse(media.status, out status))
                status = MediaStatus.UNKNOWN;

            description = media.description;

            startDate = new DateTime(media.startDate.year ?? 1, media.startDate.month ?? 1, media.startDate.day ?? 1);
            endDate = new DateTime(media.endDate.year ?? 1, media.endDate.month ?? 1, media.endDate.day ?? 1);
            seasonName = media.season;
            seasonYear = media.seasonYear ?? -1;

            episodes = media.episodes ?? -1;
            duration = media.duration ?? -1;

            coverMedium = media.coverImage.medium;
            coverLarge = media.coverImage.large;
            coverExtraLarge = media.coverImage.extraLarge;

            genres = new List<string>(media.genres);
            synonyms = new List<string>(media.synonyms);

            averageScore = media.averageScore ?? -1;
            meanScore = media.meanScore ?? -1;
            popularity = media.popularity;

            tags = new Dictionary<string, int>();
            foreach(Anilist.Result.Tag tag in media.tags)
            {
                tags.Add(tag.name, tag.rank);
            }
            isAdult = media.isAdult;

            if(media.nextAiringEpisode != null)
            {
                nextAiringEpisode = DateTimeOffset.FromUnixTimeSeconds(media.nextAiringEpisode.airingAt).DateTime;
                nextEpisodeNumber = media.nextAiringEpisode.episode;
            }
            else
            {
                nextAiringEpisode = default;
                nextEpisodeNumber = -1;
            }

            siteUrl = media.siteUrl;
            if(media.externalLinks != null)
            {
                otherLinks = new Dictionary<string, string>();
                foreach(Anilist.Result.ExternalLink link in media.externalLinks)
                {
                    otherLinks.Add(link.url, link.site);
                }
            }
        }

    }
}
