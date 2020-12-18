using Stripe;
using xperters.models.Enums;

namespace xperters.models
{
    public class Card : CreditCardOptions
    {
         public bool IsNotSupported { get; set; }
        public string Country { get; set; }
        public CardScheme CardScheme { get; set; }
        public CardType CardType { get; set; }
    
    }
}
