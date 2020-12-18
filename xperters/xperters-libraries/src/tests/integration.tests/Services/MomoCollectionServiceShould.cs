using System;
using System.Threading.Tasks;
using xperters.payments.Services;
using xperters.payments.Services.Models.Internal;
using Xunit;

namespace xperters.integration.tests.Services
{
    public class MomoCollectionServiceShould : BaseTests
    {
        private readonly Guid _externalId;
        private bool _requestSent;
        private const string TestNumberFailed = "46733123450";
        private const string TestNumberRejected = "46733123451";
        private const string TestNumberTimeout = "46733123452";
        private const string TestNumberOngoing = "46733123453";
        private const string TestNumberPending = "46733123454"; 

        public MomoCollectionServiceShould()
        {
            _externalId = Guid.NewGuid();
        }

        [Fact]
        public async Task RequestToPay()
        {
            var requestPayer = new RequestPayer
            {                
                Payer = new Payer
                {
                    PartyId = "256772123456"
                },
                ExternalId = _externalId.ToString(),
                Note = "dd",
                Message = "dd",
                Currency  = "EUR",
                Amount = 600
            };

            var service = new MomoCollectionService(Config, LoggerFactory);

            await service.MakeRequestToPayAsync(requestPayer);

            _requestSent = true;

        }

        [Fact]
        public async void GetRequestToPayStatusAsync()
        {
            if (ParametersNotValid())
            {
                await RequestToPay();
                if (ParametersNotValid())
                {
                    return;
                }

            }

            var service = new MomoCollectionService(Config, LoggerFactory);
            var result = await service.GetRequestToPayStatusAsync(_externalId);

            Guid.TryParse(result.ExternalId, out var externalId );

            Assert.NotEqual(Guid.Empty, externalId);
        }

        private bool ParametersNotValid()
        {
            return !_requestSent || _externalId == Guid.Empty || _externalId == Guid.Empty;
        }
    }
}
