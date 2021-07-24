using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes.AnimeApp
{
    public class Tag
    {
        public string tagName;
        public int tagRank;

        public string getTagRankFormated()
        {
            return tagRank + "%";
        }
    }

    public class Media
    {
        #region Attributes
        public int id;                                                  //The Anilist ID of the media

        public string title;                                            //The user prefered title of the media
        public List<string> altTitles;                                  //List of other titles that the media can be called
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

        public int averageScore;                                        //The average score of all users on Anilist (if not avaliable, set to -1)
        public int meanScore;                                           //The mean score of all users on Anilist (if not avaliable, set to -1)
        public int popularity;                                          //How many users have this media in their lists

        public List<Tag> tags;                                          //The list of tags of this media
        public bool isAdult;                                            //This media is +18 and should probably be hiiden

        public DateTime nextAiringEpisode;                              //The date of next episode (if avaliable)
        public int nextEpisodeNumber;                                   //The number of the next episode (if not avaliable, set to -1)

        public List<string> studios;                                    //The studios involved in the production of the media
        public string siteUrl;                                          //Link to the media official site (if avaliable)
        public Dictionary<string, string> otherLinks;                   //Links related to the media (if avaliable)

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

        public string GetEpisodesAndDurationFormated()
        {
            string s;

            if (episodes != -1)
                s = string.Format("{0} episode{1}", episodes, episodes != 1 ? "s" : "");
            else if (nextAiringEpisode > DateTimeOffset.FromUnixTimeSeconds(0).DateTime && nextEpisodeNumber != -1)
                s = string.Format("{0} aired episode{1}", nextEpisodeNumber - 1 != 0 ? nextEpisodeNumber - 1 : 1, nextEpisodeNumber != 2 ? "s" : "");
            else
                s = "Unknown";

            if (duration > 0)
                s += string.Format(", {0} minute{1} long", duration, duration != 1 ? "s" : "");

            return s;
        }

        public string GetStartDateFormated()
        {
            if (startDate == default)
                return "Unknown";

            return startDate.ToString("dd/MM/yyyy");
        }

        public string GetEndDateFormated()
        {
            if (endDate == default)
                return "Unkown";

            return endDate.ToString("dd/MM/yyyy");
        }

        public string GetMediaStatusFormated()
        {
            switch(status)
            {
                case MediaStatus.Not_Yet_Released:
                    return "Not aired yet";
                case MediaStatus.Releasing:
                    return "Currently airing";
                case MediaStatus.Finished:
                    return "Finished airing";
                case MediaStatus.Cancelled:
                    return "Cancelled";
                default:
                    return "Unknown";
            }
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
            altTitles = new List<string>();

            foreach (string synonym in media.synonyms)
                altTitles.Add(synonym);

            format = media.format;
            if (!Enum.TryParse(media.status, out status))
                status = MediaStatus.Unknown;

            description = media.description;

            episodes = media.episodes ?? -1;
            duration = media.duration ?? -1;

            genres = new List<string>(media.genres);

            tags = new List<Tag>();
            foreach(Anilist.Result.Tag tag in media.tags)
            {
                tags.Add(new Tag() { tagName = tag.name, tagRank = tag.rank } );
            }
            isAdult = media.isAdult;

            studios = new List<string>();
            foreach(Anilist.Result.Node node in media.studios.nodes)
            {
                studios.Add(node.name);
            }
            siteUrl = media.siteUrl;
        }

    }
}
