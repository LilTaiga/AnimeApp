using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace AnimeApp.Classes.Anilist
{
    //Class made to handle all HTTP requests made to Anilist.
    public static class AnilistRequest
    {
        //Singleton made to generate only one HttpClient object.
        private static HttpClient http;
        private static HttpClient Http
        {
            get
            {
                if(http == null)
                    http = new HttpClient();

                return http;
            }
        }

        //Constructs the http content to be sent into the http message.
        private static string ConstructHttpContent(string body, string variables)
        {
            //Remove any linebreaks to avoid conflit with GraphQL
            body = body.Replace('\r', ' ').Replace('\n', ' ');
            variables = variables.Replace('\r', ' ').Replace('\n', ' ');

            //Returns the formated content to be sent.
            return string.Format("{{\"query\": \"{0}\", \"variables\": {{{1}}}}}", body, variables);
        }

        //Send a GraphQL request to Anilist
        public async static Task<string> SendQuery(string body, string variables)
        {
            //Setup the GraphQL message to be sent, based on body and variables contents.
            string content = ConstructHttpContent(body, variables);

            //Send the http request to Anilist.
            //Raises an exception if it cannot receive back a response.
            try
            {
                var response = await SendHttpRequest(content);
                return response.Content.ToString();
            }
            catch(Exception e)
            {
                throw new Exception("Error getting a response from Anilist servers.", e);
            }
        }

        //Send a GraphQL request to Anilist using user's token as authorization.
        public async static Task<string> SendQueryAuthorized(string body, string variables, string authToken = null)
        {
            //If there's no token, then it can't send the authorized message.
            if (AnilistAccount.Token == null && authToken == null)
                throw new Exception("No user authenticated.");

            //Setup the GraphQL message to be sent, based on body and variables contents.
            string content = ConstructHttpContent(body, variables);

            //Send the http request to Anilist.
            //Raises an exception if it cannot receive back a response.
            try
            {
                var response = await SendHttpRequest(content, authToken ?? AnilistAccount.Token);
                return response.Content.ToString();
            }
            catch(Exception e)
            {
                throw new Exception("Error getting a response from Anilist servers.", e);
            }
        }

        //Constructs the http message to be sent into the http request.
        //Can raise an exception if server can not be reached.
        private async static Task<HttpResponseMessage> SendHttpRequest(string content, string authToken = null)
        {
            //Mounts the http message
            HttpStringContent httpContent = new HttpStringContent(content, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            //Sets the authorization token, if avaliable.
            if(authToken != null)
                Http.DefaultRequestHeaders.Authorization = HttpCredentialsHeaderValue.Parse(authToken);

            //send the http request to Anilist.
            //Raises an exception if it cannot receive back a response, or response was unsuccessful.
            try
            {
                var response =  await Http.PostAsync(new Uri("https://graphql.anilist.co/"), httpContent);
                return response.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                throw new Exception("Server could not be reached.", e);
            }
        }
    }
}
