using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace AnimeApp.Classes.Utilities
{
    //Class for handling exchange of data between an http point.
    public static class HttpConnection
    {
        //Singleton pattern.
        private static HttpClient client = new HttpClient();

        //Download an image from the web
        //The image is always stored in the local cache folder, in the 'images' folder.
        public static async Task DownloadImageAsync(string _filename, Uri _uri)
        {
            //Checks if the provided link is indeed an image.
            string extension = Path.GetExtension(_uri.AbsoluteUri);

            if (extension != ".png" && extension != ".jpeg" && extension != ".jpg")
                throw new ArgumentException("Provided link is not an image");

            var imageContent = await client.GetByteArrayAsync(_uri.AbsoluteUri);
            await FileHandler.WriteBytesToLocalCacheFolder("images\\" + _filename + extension, imageContent);
        }


    }
}
