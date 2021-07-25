using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    public abstract class Media
    {

        #region Properties

        public int Id { get; set; }                             //The Anilist ID of the media

        public MediaTitle Titles { get; set; }                  //All the titles and alternative titles of the media
        public string Description { get; set; }                 //The description of the media

        public MediaStatus Status { get; set; }                 //The current status of the media

        public DateTime? StartDate { get; set; }                 //The date when the media started releasing
        public bool StartDateYearOnly { get; set; }             //The start date only contains the year
        public DateTime? EndDate { get; set; }                   //The date when the media was finished
        public bool EndDateYearOnly { get; set; }               //The end date only contains the year

        public int Updated { get; set; }                        //The last time when this media information was updated on Anilist

        public MediaImage Cover { get; set; }                   //The image of the media's cover
        public MediaImage Banner { get; set; }                  //The image of the media's banner in Anilist

        public List<string> Genres { get; set; }                //A list of all genres of this media
        public List<MediaTag> Tags { get; set; }                //A list of all tags of this media

        public int AverageScore { get; set; }                   //The average score of this media in Anilist (-1 if unavaliable)
        public int Popularity { get; set; }                     //The quantity of users that have this media in their lists (-1 if unavaliable)

        public bool IsAdult { get; set; }                       //If this media is +18 only

        public string SiteUrl { get; set; }                     //The link for the Anilist page of this media

        #endregion

        //
        // Constructors
        //

        //Default constructor
        //Should be edited externally
        public Media()
        {

        }

        //This constructor parses an Anilist Result into this class.
        protected Media(Anilist.Result.Media _media)
        {
            Id = _media.id;

            Titles = new MediaTitle
            {
                English = _media.title.english,
                Romaji = _media.title.romaji,
                Native = _media.title.native,
                Synonyms = new List<string>()
            };
            foreach (string synonym in _media.synonyms)
                Titles.Synonyms.Add(synonym);

            Description = _media.description;
            Status = Enum.Parse<MediaStatus>(_media.status, true);
            Updated = _media.updatedAt;
            AverageScore = _media.averageScore ?? -1;
            Popularity = _media.popularity;
            IsAdult = _media.isAdult;
            SiteUrl = _media.siteUrl;

            #region Start and End Date bullshit

            if(_media.startDate.year.HasValue)
            {
                StartDate = new DateTime(_media.startDate.year ?? 1,
                                         _media.startDate.month ?? 1,
                                         _media.startDate.day ?? 1);

                StartDateYearOnly = !_media.startDate.month.HasValue;
            }

            if (_media.endDate.year.HasValue)
            {
                EndDate = new DateTime(_media.endDate.year ?? 1,
                                         _media.endDate.month ?? 1,
                                         _media.endDate.day ?? 1);

                EndDateYearOnly = !_media.endDate.month.HasValue;
            }
                
            #endregion

            Cover = new MediaImage(_media.coverImage.large);
            Banner = new MediaImage(_media.bannerImage);

            Genres = new List<string>();
            foreach (string genre in _media.genres)
                Genres.Add(genre);

            Tags = new List<MediaTag>();
            foreach(Anilist.Result.Tag tag in _media.tags)
            {
                MediaTag _tag = new MediaTag
                {
                    Name = tag.name,
                    Rank = tag.rank,
                    IsSpoiler = tag.isMediaSpoiler
                };

                Tags.Add(_tag);
            }
        }
    }
}
