using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace AnimeApp.Classes.Utilities
{
    public static class JsonHandler
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        { 
            WriteIndented = true,
            IncludeFields = true
        };

        public static string FromObjectToJson<T>(T _object)
        {
            var jsonContent = JsonConvert.SerializeObject(_object, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return jsonContent;
        }

        public static T FromJsonToObject<T>(string _jsonString)
        {
            return JsonConvert.DeserializeObject<T>(_jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
