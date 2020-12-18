
using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Internal
{
    public class Payer
    {
        [JsonProperty(PropertyName = "partyIdType")]
        public string PartyIdType => "MSISDN";

        [JsonProperty(PropertyName = "partyId")]
        public string PartyId { get; set; }
    }
}
