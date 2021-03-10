using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace AnimeApp.Classes.Anilist
{
    public static class AnilistRequest
    {
        private static HttpClient _http;
        private static HttpClient http
        {
            get
            {
                if(_http == null)
                    _http = new HttpClient();

                return _http;
            }
        }

        public async static Task<string> SendQuery(string body, string variables)
        {
            body = body.Replace('\r', ' ').Replace('\n', ' ');
            variables = variables.Replace('\r', ' ').Replace('\n', ' ');

            string request = string.Format("{{\"query\": \"{0}\", \"variables\": {{{1}}}}}", body, variables);

            HttpStringContent content = new HttpStringContent(request, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            var response = await http.PostAsync(new Uri("https://graphql.anilist.co/"), content);

            return response.Content.ToString();
        }

        public async static Task<string> SendQueryAuthorized(string body, string variables, string authToken = null)
        {
            string token;
            if (authToken != null)
                token = authToken;
            else if (AnilistAccount.GetInstance().token != null)
                token = AnilistAccount.GetInstance().token;
            else
                throw new Exception("User not authorized.");

            string request = string.Format("{{\"query\": \"{0}\", \"variables\": {{{1}}}}}", body, variables);
            HttpStringContent content = new HttpStringContent(request, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            http.DefaultRequestHeaders.Authorization = HttpCredentialsHeaderValue.Parse(token);

            var response = await http.PostAsync(new Uri("https://graphql.anilist.co/"), content);

            return response.Content.ToString();
        }
    }
}
