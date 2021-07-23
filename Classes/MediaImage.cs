using Windows.Storage;

namespace AnimeApp.Classes
{
    public class MediaImage
    {
        public bool IsDownloaded { get; set; }          //The stored image cover of the media
        public string WebUrl { get; set; }              //The Anilist link to the image cover of the media
    }
}
