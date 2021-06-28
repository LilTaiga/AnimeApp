using System;
using System.Collections.Generic;
using System.Globalization;

#pragma warning disable IDE1006
namespace AnimeApp.Classes.Anilist.Result
{
    public class AnilistResponse
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Viewer Viewer { get; set; }
        public User User { get; set; }
        public MediaListCollection MediaListCollection { get; set; }
    }

    public class Viewer
    {
        public int id { get; set; }
        public string name { get; set; }
        public Avatar avatar { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public Avatar avatar { get; set; }
    }

    public class Avatar
    {
        public string medium { get; set; }
        public string large { get; set; }
    }

    public class Title : IComparable<Title>
    {
        public string romaji { get; set; }
        public string english { get; set; }
        public string native { get; set; }
        public string userPreferred { get; set; }

        public int CompareTo(Title _other)
        {
            return string.Compare(romaji, _other.romaji, true);
        }
    }

    public class StartDate : IComparable<StartDate>
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }

        public int CompareTo(StartDate other)
        {
            DateTime date = new DateTime(year ?? 1, month ?? 1, day ?? 1);
            DateTime otherDate = new DateTime(other.year ?? 1, other.month ?? 1, other.day ?? 1);

            return date.CompareTo(otherDate);
        }
    }

    public class EndDate
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class CoverImage
    {
        public string color { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string extraLarge { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int rank { get; set; }
        public bool isGeneralSpoiler { get; set; }
        public bool isMediaSpoiler { get; set; }
        public bool isAdult { get; set; }
    }

    public class NextAiringEpisode
    {
        public int airingAt { get; set; }
        public int timeUntilAiring { get; set; }
        public int episode { get; set; }
    }

    public class ExternalLink
    {
        public string url { get; set; }
        public string site { get; set; }
    }

    public class Media
    {
        public int id { get; set; }
        public Title title { get; set; }
        public string format { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public StartDate startDate { get; set; }
        public EndDate endDate { get; set; }
        public string season { get; set; }
        public int? seasonYear { get; set; }
        public int? seasonInt { get; set; }
        public int? episodes { get; set; }
        public int? duration { get; set; }
        public CoverImage coverImage { get; set; }
        public List<string> genres { get; set; }
        public List<string> synonyms { get; set; }
        public int? averageScore { get; set; }
        public int? meanScore { get; set; }
        public int popularity { get; set; }
        public List<Tag> tags { get; set; }
        public bool isAdult { get; set; }
        public NextAiringEpisode nextAiringEpisode { get; set; }
        public string siteUrl { get; set; }
        public List<ExternalLink> externalLinks { get; set; }

        public double GetNumberOfAiredEpisodes()
        {
            return episodes ?? (nextAiringEpisode != null ? nextAiringEpisode.episode : 1);
        }

        public string GetMediaFormat()
        {
           string newFormat =  format.Replace('_', ' ').ToLower().Replace("tv", "TV").Replace("ova", "OVA").Replace("ona", "ONA");
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newFormat);
        }

        public string GetSeasonAndYear()
        {
            string SeasonAndYear = $"{season ?? "Unknown"} {startDate.year}";

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SeasonAndYear.ToLower());
        }
    }

    public class StartedAt : IComparable<StartedAt>
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }

        public int CompareTo(StartedAt other)
        {
            DateTime date = new DateTime(year ?? 1, month ?? 1, day ?? 1);
            DateTime otherDate = new DateTime(other.year ?? 1, other.month ?? 1, other.day ?? 1);

            return date.CompareTo(otherDate);
        }
    }

    public class CompletedAt : IComparable<CompletedAt>
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }

        public int CompareTo(CompletedAt other)
        {
            DateTime date = new DateTime(year ?? 1, month ?? 1, day ?? 1);
            DateTime otherDate = new DateTime(other.year ?? 1, other.month ?? 1, other.day ?? 1);

            return date.CompareTo(otherDate);
        }
    }

    public class Entry
    {
        public int mediaId { get; set; }
        public double score { get; set; }
        public int progress { get; set; }
        public int repeat { get; set; }
        public int priority { get; set; }
        public int createdAt { get; set; }
        public int updatedAt { get; set; }
        public StartedAt startedAt { get; set; }
        public CompletedAt completedAt { get; set; }
        public Media media { get; set; }

        public string GetProgressFormatted()
        {
            return progress + "/" + (media.episodes ?? (media.nextAiringEpisode != null ? media.nextAiringEpisode.episode - 1 : 0)).ToString() + (media.episodes == null ? "+" : "");
        }
    }

    public class List
    {
        public string name { get; set; }
        public string status { get; set; }
        public bool isCustomList { get; set; }
        public List<Entry> entries { get; set; }
    }

    public class MediaListCollection
    {
        public List<List> lists { get; set; }
    }
}
#pragma warning restore IDE1006
