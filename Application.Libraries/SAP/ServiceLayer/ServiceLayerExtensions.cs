using Application.Libraries.SAP.SL;
using Application.Libraries.Utilies.Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.SAP.ServiceLayer
{
    public static class ServiceLayerExtensions
    {
        public static async Task<T> GetAsync<T>(this SLRequest request, JsonSerializerSettings serializerSettings, bool unwrapCollection = true)
        {
            T resp;
            using (Stream s = request.GetStreamAsync().Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = JsonSerializer.Create(serializerSettings);
                // read the json from a stream
                // json size doesn't matter because only a small piece is read at a time from the HTTP request
                resp = serializer.Deserialize<T>(reader);
                return resp;
            }
            return default;
        }

        public static JsonSerializerSettings SerializerSettings => new JsonSerializerSettings()
        {

            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new SapPropertyContractResolver(),
            Converters = new JsonConverter[]
            {
                new SapConverters(),
                new SapDateConverter()
            }
        };
    }
}
