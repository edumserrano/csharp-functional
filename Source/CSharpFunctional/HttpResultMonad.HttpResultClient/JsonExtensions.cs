using Newtonsoft.Json;

namespace HttpResultMonad.HttpResultClient
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public static T DeserializeObject<T>(this string json)
        {
            var deserializedObject = JsonConvert.DeserializeObject<T>(json, _settings);
            return deserializedObject;
        }
    }
}