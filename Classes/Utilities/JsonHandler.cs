using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace AnimeApp.Classes.Utilities
{
    public static class JsonHandler
    {
        public static byte[] FromObjectToJson<T>(T _object)
        {
            var options = new JsonSerializerOptions() { WriteIndented = true };
            var jsonContent = JsonSerializer.SerializeToUtf8Bytes(_object, options);

            return jsonContent;
        }

        public static T FromJsonToObject<T>(string _jsonString)
        {
            return JsonSerializer.Deserialize<T>(_jsonString);
        }
    }
}
