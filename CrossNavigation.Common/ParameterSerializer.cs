using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CrossNavigation.Common
{
    public class ParameterSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            Converters = {new IsoDateTimeConverter(), new StringEnumConverter()},
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            TypeNameHandling = TypeNameHandling.All
        };

        public async Task<T> DeserializeAsync<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString)) throw new ArgumentException("Parameter must not be null or empty", nameof(jsonString));
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(jsonString, _settings))
                .ConfigureAwait(false);
        }

        public async Task<string> SerializeAsync(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return await Task.Run(() => JsonConvert.SerializeObject(obj, _settings))
                .ConfigureAwait(false);
        }
    }
}