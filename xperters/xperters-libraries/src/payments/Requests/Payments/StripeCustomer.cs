using System;
using System.Collections.Generic;
using System.Text;

namespace xperters.payments.Requests.Payments
{
    public class StripeCustomer
    {
        public StripeCustomer()
        {
            
        }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
