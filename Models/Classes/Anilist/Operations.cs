using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;
using Windows.ApplicationModel.Resources;
using AnimeApp.Classes.Anilist.Result;

namespace AnimeApp.Classes.Anilist
{
    //This class has all operations that AnimeApp can request to Anilist.
    public static class Operations
    {
        #region Unauthorized

        //Searches for a particular username in Anilist.
        //Can only receive back information if user's information is public.
        //Used only to verify the existence of said user.
        public async static Task<AnilistResponse> SearchForUser(string _username)
        {
            //Retrieves the GraphQL body message to perform this operation.
            var resource = ResourceLoader.GetForCurrentView("AnilistQueries");
            string body = resource.GetString("UserFetch");

            //Constructs the GraphQL variables message to perform this operation.
            string variables = "\"name\": \" "+ _username +"\"";

            //Send the GrapQL message to Anilist, and awaits for a response.
            //Can raise an exception if unsuccessfull.
            try
            {
                string result = await Request.SendQuery(body, variables);
                AnilistResponse data = Utilities.JsonHandler.FromJsonToObject<AnilistResponse>(result);

                return data;
            }
            catch(Exception e)
            {
                throw new Exception("Couldn't retrieve user information.", e);
            }
        }

        #endregion



        #region Authorized

        //If parameter is not null, then verifies the authenticity of the current user.
        //If parameter is null, retrieves the current user's information.
        //Only call this method if the user is logged in, otherwise will raise an exception.
        public async static Task<AnilistResponse> GetViewer(string _token = null)
        {
            //Retrieves the GraphQL body message to perform this operation.
            var resource = ResourceLoader.GetForCurrentView("AnilistQueries");
            string body = resource.GetString("ViewerFetch");

            //Send the GrapQL message to Anilist, and awaits for a response.
            //Can raise an exception if unsuccessfull.
            try
            {
                string result = await Request.SendQuery(body, "", _token);
                AnilistResponse data = Utilities.JsonHandler.FromJsonToObject<AnilistResponse>(result);

                return data;
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't retrieve user information.", e);
            }
        }

        //Retrieves the current authenticated user's anime lists.
        //Only call this method if the user is logged in, otherwise it will return null.
        public async static Task<AnilistResponse> GetUserAnimeLists(UserAccount _acc)
        {
            if (_acc == null || _acc.Anilist.AuthToken == null)
                return null;

            //Retrieves the GraphQL body message to perform this operation.
            var resource = ResourceLoader.GetForViewIndependentUse("AnilistQueries");
            string body = resource.GetString("MediaListCollectionFetch");
            string variables = string.Format("\"userId\": {0}", _acc.Anilist.Id);

            //Send the GrapQL message to Anilist, and awaits for a response.
            //Can raise an exception if unsuccessfull.
            try
            {
                string result = await Request.SendQuery(body, variables, _acc.Anilist.AuthToken);
                AnilistResponse data = Utilities.JsonHandler.FromJsonToObject<AnilistResponse>(result);

                return data;
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't retrieve user information.", e);
            }
        }

        #endregion
    }
}
