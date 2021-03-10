﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Windows.Storage;

namespace AnimeApp.Classes.Anilist
{
    public class AnilistQuery
    {
        public async static Task<AnilistResult> SearchForUser(string _username)
        {
            string body = "query($name: String){User(search: $name){id name avatar{medium large}}}";
            string variables = "\"name\": \" "+ _username +"\"";

            string result = await AnilistRequest.SendQuery(body, variables);
            AnilistResult data = JsonSerializer.Deserialize<AnilistResult>(result);

            return data;
        }

        public async static Task<AnilistResult> GetViewer(string _token)
        {
            var resource = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("AnilistQueries");
            string body = resource.GetString("ViewerFetch");

            string result = await AnilistRequest.SendQueryAuthorized(body, "", _token);
            AnilistResult data = JsonSerializer.Deserialize<AnilistResult>(result);

            return data;
        }

        public async static Task<AnilistResult> GetViewerDetails()
        {
            var resource = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView("AnilistQueries");
            string body = resource.GetString("MediaListCollectionFetch");

            var result = await AnilistRequest.SendQueryAuthorized(body, string.Format("\"id\": {0}", AnilistAccount.Id));
            var data = JsonSerializer.Deserialize<AnilistResult>(result);

            return data;
        }
    }
}