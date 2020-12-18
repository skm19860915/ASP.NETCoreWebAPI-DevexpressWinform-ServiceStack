using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace xperters.payments.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson(this object value)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };

            return JsonConvert.SerializeObject(value, settings);
        }

        public static T FromJson <T>(this string value)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };

            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }
}
