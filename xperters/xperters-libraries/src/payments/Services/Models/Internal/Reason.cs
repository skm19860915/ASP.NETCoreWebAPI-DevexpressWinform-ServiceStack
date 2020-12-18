
using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Internal
{
    public class Reason
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
