using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using xperters.infrastructure.AzureB2C;
using Xunit;

namespace xperters.unit.tests.SignIn
{
    public class SignInTests : BaseUnitTests
    {

        [Fact]
        public void SignInAfterResponse()
        {
            var option = new OpenIdConnectOptions();

            var services = new ServiceCollection();
            var setup = new AzureAdB2CAuthenticationBuilderExtensions.OpenIdConnectOptionsSetup(AppConfig.AzureAdB2CSettings,  LoggerFactory);
            setup.Configure(option);

            // Next need to call the event OnSecurityTokenValidated
        }
    }
}
