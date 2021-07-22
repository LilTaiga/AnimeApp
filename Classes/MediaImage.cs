using Windows.Storage;

namespace AnimeApp.Classes
{
    class MediaImage
    {
        StorageFile imageFile;              //The stored image cover of the media
        string ImageWebUrl;                 //The Anilist link to the image cover of the media
        char[] color;                       //The hex code of the average color of the cover
    }
}
