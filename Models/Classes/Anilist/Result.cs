using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

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

    public class Title
    {
        public string english { get; set; }
        public string romaji { get; set; }
        public string native { get; set; }
    }

    public class StartDate
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class EndDate
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class CoverImage
    {
        public string large { get; set; }
        public string color { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public int rank { get; set; }
        public bool isMediaSpoiler { get; set; }
    }

    public class AiringSchedule
    {
        public List<Node> nodes { get; set; }
    }

    public class Media
    {
        public int id { get; set; }
        public Title title { get; set; }
        public List<string> synonyms { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public StartDate startDate { get; set; }
        public EndDate endDate { get; set; }
        public int updatedAt { get; set; }
        public CoverImage coverImage { get; set; }
        public string bannerImage { get; set; }
        public List<string> genres { get; set; }
        public List<Tag> tags { get; set; }
        public int? averageScore { get; set; }
        public int popularity { get; set; }
        public bool isAdult { get; set; }
        public Studios studios { get; set; }
        public string siteUrl { get; set; }
        public string format { get; set; }
        public int? episodes { get; set; }
        public int? duration { get; set; }
        public AiringSchedule airingSchedule { get; set; }
    }

    public class Studios
    {
        public List<Node> nodes { get; set; }
    }

    public class Node
    {
        public string name { get; set; }
        public bool isAnimationStudio { get; set; }
        public int episode { get; set; }
        public int airingAt { get; set; }
    }

    public class StartedAt
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class CompletedAt
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class Entry
    {
        public int id { get; set; }
        public string status { get; set; }
        public double score { get; set; }
        public int progress { get; set; }
        public int repeat { get; set; }
        [JsonPropertyName("private")]
        public bool isPrivate { get; set; }
        public string notes { get; set; }
        public StartedAt startedAt { get; set; }
        public CompletedAt completedAt { get; set; }
        public int createdAt { get; set; }
        public int updatedAt { get; set; }
        public Media media { get; set; }
    }

    public class List
    {
        public string name { get; set; }
        public string status { get; set; }
        public bool isCustomList { get; set; }
        public bool isSplitCompletedList { get; set; }
        public List<Entry> entries { get; set; }
    }

    public class MediaListCollection
    {
        public List<List> lists { get; set; }
    }
}
#pragma warning restore IDE1006
