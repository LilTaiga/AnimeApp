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

        //Constructs the GraphQL query to be sent in the http message..
        private static string ConstructHttpContent(string _body, string _variables)
        {
            //Remove any linebreaks to avoid conflits with GraphQL
            _body = _body.Replace('\r', ' ').Replace('\n', ' ');
            _variables = _variables.Replace('\r', ' ').Replace('\n', ' ');

            //Returns the formated content to be sent.
            //Due to unnecessary complations made by string.Format, string has to be written this way.
            //Basically means:
            /*{
             *   "query": "body",
             *   "variables": {
             *     variables
             *   }
             * }
             */
            return string.Format("{{\"query\": \"{0}\", \"variables\": {{{1}}}}}", _body, _variables);
        }

        //Send a GraphQL request to Anilist
        public async static Task<string> SendQuery(string _body, string _variables, string _authToken = null)
        {
            //Setup the GraphQL message to be sent, based on body and variables contents.
            string content = ConstructHttpContent(_body, _variables);

            //Send the http request to Anilist.
            //Raises an exception if it cannot receive back a response.
            try
            {
                var response = await Utilities.HttpConnection.SendGraphQLQuery("https://graphql.anilist.co/", content, _authToken);
                return response;
            }
            catch(Exception e)
            {
                throw new Exception("Error getting a response from Anilist servers.", e);
            }
        }

        /*//Send a GraphQL request to Anilist using user's token as authorization.
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
        }*/
    }
}
