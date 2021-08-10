using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    public class Entry
    {
        #region Properties

        public int Id { get; set; }
        public EntryStatus Status { get; set; }
        public int Score { get; set; }
        public int Progress { get; set; }
        public int Repeat { get; set; }
        public bool IsPrivate { get; set; }
        public string Notes { get; set; }

        public DateTime Started { get; set; }
        public DateTime Completed { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Media Media { get; set; }

        #endregion

        //
        // Constructors
        //

        //Default constructor
        //Should be edited externally
        public Entry()
        {

        }

        //This constructor parses an Anilist Result into this class.
        public Entry(Anilist.Result.Entry _entry)
        {
            Id = _entry.id;
            Status = Enum.Parse<EntryStatus>(_entry.status, true);
            Score = (int)_entry.score;
            Progress = _entry.progress;
            Repeat = _entry.repeat;
            IsPrivate = _entry.isPrivate;
            Notes = _entry.notes;

            #region Started and Completed

            if (_entry.startedAt.year == null)
                Started = default;
            else
                Started = new DateTime(_entry.startedAt.year ?? 1,
                                       _entry.startedAt.month ?? 1,
                                       _entry.startedAt.day ?? 1);

            if (_entry.completedAt.year == null)
                Completed = default;
            else
                Completed = new DateTime(_entry.completedAt.year ?? 1,
                                       _entry.completedAt.month ?? 1,
                                       _entry.completedAt.day ?? 1);

            #endregion

            Created = DateTimeOffset.FromUnixTimeSeconds(_entry.createdAt).DateTime;
            Updated = DateTimeOffset.FromUnixTimeSeconds(_entry.updatedAt).DateTime;

            Media = new Anime(_entry.media);
        }

        

        public string GetAnimeProgressFormatted()
        {
            Anime anime = Media as Anime;
            string progress = "{0}/{1}";

            if (anime.Episodes != -1)
                return string.Format(progress, Progress, anime.Episodes);

            if (anime.NextEpisodes.Count == 0)
                return string.Format(progress, Progress, "??");

            return string.Format(progress, Progress, anime.NextEpisodes[0].Episode - 1 + "+");
        }
    }
}
