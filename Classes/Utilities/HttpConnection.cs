using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

using System.Runtime.InteropServices.WindowsRuntime;
using System.Net;

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

            var imageContent = await client.GetByteArrayAsync(_uri);
            await FileHandler.WriteBytesToLocalCacheFolder("images\\" + _filename + extension, imageContent);
        }

        //Send an Http Request to a GraphQL server.
        public static async Task<string> SendGraphQLQuery(string _webUrl, string _content, string _authCode = null)
        {
            //Setting up http content and headers.
            StringContent content = new StringContent(_content, System.Text.Encoding.UTF8, "application/json");
            if (_authCode != null)
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authCode);

            try
            {
                //Send POST request to the server.
                //After that, remove auth token from http immediately.
                var response = await client.PostAsync(new Uri(_webUrl), content);
                client.DefaultRequestHeaders.Authorization = null;

                //If request is sucessful, return the content of the response.
                //Otherwise, throw an exception explaining why it failed.
                if (response.IsSuccessStatusCode)
                    return response.Content.ToString();

                switch(response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new FormatException("Oopsie Whoopsie, we've made a fucky wucky uwu!");
                    case HttpStatusCode.Forbidden:
                        throw new UnauthorizedAccessException("Missing auth token");
                    default:
                        int statusCode = (int)response.StatusCode;
                        throw new NotImplementedException(statusCode.ToString());

                }
            }
            catch(HttpRequestException e)
            {
                //This exception is throw by HTTP client, when the connection was unsucessful.
                //Caused by network problems.
                //User may not be connected to the internet, ot DNS errors, or timeout.
                client.DefaultRequestHeaders.Authorization = null;
                throw new Exception("Unable to contact server.", e);
            }
            catch(Exception e)
            {
                //Catches the exceptions throw by me.
                client.DefaultRequestHeaders.Authorization = null;
                throw new Exception("Something got wrong while contacting server.", e);
            }
        }
    }
}
