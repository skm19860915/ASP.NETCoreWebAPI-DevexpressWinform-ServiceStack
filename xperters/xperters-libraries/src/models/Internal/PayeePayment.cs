
using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Internal
{
    public class PayeePayment
    {
        [JsonProperty(PropertyName = "partyIdType")]
        public string PartyIdType { get; set; }

        [JsonProperty(PropertyName = "partyId")]
        public string PartyId { get; set; }
    }
}
