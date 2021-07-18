using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes.AnimeApp
{
    //Class that stores information related to the entries of user.
    public class UserEntry
    {
        #region Properties
        public double score;           //The score given by the user to the media
        public int progress;           //How many episodes the user has watched
        public int repeat;             //How many times the user has rewatched this media
        public DateTime created;       //The date when this entry was added
        public DateTime updated;       //The date when this entry was last updated
        public DateTime started;       //The date which the user started this media
        public DateTime ended;         //The date which the user ended this media

        public EntryStatus status;

        public Media Media;            //The media
        #endregion



        #region Methods
        public string GetProgressFormated()
        {
            (int totalEpisodes, bool isEpisodesNumberKnown) = Media.GetEpisodes();

            string progress = "{0}/{1}";
            if (isEpisodesNumberKnown)
                return string.Format(progress, this.progress, totalEpisodes);
            else if (totalEpisodes != -1)
                return string.Format(progress, this.progress, totalEpisodes + "+");
            else
                return string.Format(progress, this.progress, " ? ");
        }

        public int GetStatusInteger()
        {
            if (status == EntryStatus.UNKNOWN)
                return -1;

            return ((int)status) - 1;
        }

        public DateTime? GetStartDate()
        {
            if (started == default)
                return null;

            return started;
        }

        public DateTime? GetEndDate()
        {
            if (ended == default)
                return null;

            return ended;
        }
        #endregion



        //
        //Constructor
        //

        public UserEntry(Anilist.Result.Entry entry)
        {
            score = entry.score;
            progress = entry.progress;
            repeat = entry.repeat;
            created = DateTimeOffset.FromUnixTimeSeconds(entry.createdAt).DateTime;
            updated = DateTimeOffset.FromUnixTimeSeconds(entry.updatedAt).DateTime;

            started = new DateTime(entry.startedAt.year ?? 1, entry.startedAt.month ?? 1, entry.startedAt.day ?? 1);
            ended = new DateTime(entry.completedAt.year ?? 1, entry.completedAt.month ?? 1, entry.completedAt.day ?? 1);

            Media = new Media(entry.media);
        }

    }
}
