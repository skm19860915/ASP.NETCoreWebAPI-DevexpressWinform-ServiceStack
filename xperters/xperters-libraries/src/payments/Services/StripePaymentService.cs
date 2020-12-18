using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Stripe;
using xperters.configurations.Settings.Payments;
using xperters.payments.Requests.Payments;

namespace xperters.payments.Services
{
    public class StripePaymentService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly ILogger _logger;

        public StripePaymentService(StripeSettings stripeSettings, ILoggerFactory loggerFactory)
        {
            _stripeSettings = stripeSettings;
            _logger = loggerFactory.CreateLogger<StripePaymentService>();
        }

        public Charge MakeCharge(models.Card card, Transaction transaction)
        {
            StripeConfiguration.SetApiKey(_stripeSettings.SecretKey);

            //Assign Card to Token Object and create Token  
            var tokenOptions = new TokenCreateOptions {Card = card };
            var serviceToken = new TokenService();
            var token = serviceToken.Create(tokenOptions);

            //Create Customer Object and Register it on Stripe  
            var userId = Guid.NewGuid().ToString();
            var customer = new CustomerCreateOptions {Email = transaction.Email, Source = token.Id};
            var customerService = new CustomerService();
            var stripeCustomer = customerService.Create(customer);

            //Create Charge Object with details of Charge  
            var options = new ChargeCreateOptions
            {
                Amount = transaction.Amount,
                Currency = transaction.Currency,
                ReceiptEmail = transaction.Email,
                CustomerId = stripeCustomer.Id,
                Description = Convert.ToString(DateTime.Now.ToBinary()), //Optional  
            };

            //and Create Method of this object is doing the payment execution.  
            var service = new ChargeService();
            var charge = service.Create(options); // This will do the Payment

            var transactionAmount = ((decimal)transaction.Amount / 100).ToString(CultureInfo.InvariantCulture);
            _logger.LogInformation($"Charge made successfully on behalf of {userId} for {transaction.Currency}{transactionAmount}. Auth code: {charge.AuthorizationCode}");

            return charge;
        }
    }
}
