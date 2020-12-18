using System.Threading.Tasks;
using xperters.azuread.Handlers;
using xperters.azuread.Interfaces;
using xperters.azuread.Requests;
using xperters.enums;
using xperters.extensions;
using Xunit;

namespace xperters.integration.tests.Handlers
{
    public class AdAuthHandlerShould : BaseTests
    {

        [Fact(Skip = "Not working")]
        public async Task<UserDetailsRequest> GetAadUserDetails()
        {
            var handler = new AdAuthHandler(Env.Object, HttpHandler, Config, LoggerFactory);
            var userDetails = await handler.GetAadUserDetails(UserAdId,Config.AzureAdB2CSettings.GraphEndpoint);

            Assert.True(userDetails.Id.IsNotBlank());
            Assert.True(userDetails.GivenName.IsNotBlank());
            Assert.True(userDetails.Surname.IsNotBlank());
            Assert.True(userDetails.DisplayName.IsNotBlank());
            Assert.True(userDetails.MobilePhone.IsNotBlank());
            Assert.True(userDetails.UserPrincipalName.IsNotBlank());

            return new UserDetailsRequest{DisplayName = userDetails.DisplayName , MobilePhone = userDetails.MobilePhone};
        }

        /// <summary>
        /// Test that update to AAD works as expected
        /// </summary>
        [Fact(Skip = "Not working")]
        public async void AddDisplayNameToAdAccount()
        {
            var handler = new AdAuthHandler(Env.Object, HttpHandler, Config, LoggerFactory);

            var displayName = "Test Joe";
            var mobileNumber = "+14445558888";

            var userDetails = new UserDetailsRequest{DisplayName = displayName, MobilePhone = mobileNumber};

            await handler.UpdateAadUserDetails(UserAdId, userDetails, Config.AzureAdB2CSettings.GraphEndpoint);
            var requestDetails = await GetAadUserDetails();

            Assert.Equal(displayName, requestDetails.DisplayName);
            Assert.Equal(mobileNumber, requestDetails.MobilePhone);

            userDetails.DisplayName = displayName;
            userDetails.MobilePhone = mobileNumber;

            // Set the name back to that which was originally there
            await handler.UpdateAadUserDetails(UserAdId, userDetails, Config.AzureAdB2CSettings.GraphEndpoint);

        }


        [Fact]
        public async void ShouldRetrieveToken()
        {
            var handler = new AdAuthHandler(Env.Object, HttpHandler, Config, LoggerFactory);
            var token = await handler.GetAccessToken();

            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact(Skip = "Not working")]
        public async void GetUserRoleValueForUserFreelancer()
        {
            const string userId = UserId;

            var handler = new AdAuthHandler(Env.Object, HttpHandler, Config, LoggerFactory);
            var role = await handler.UpdateUserRole(userId, Enums.UserRole.Freelancer);

            Assert.True(role > 0);
        }        
        
        [Fact(Skip = "Not working")]
        public async void GetUserRoleValueForUserClient()
        {
            const string userId = UserId;

            var handler = new AdAuthHandler(Env.Object, HttpHandler, Config, LoggerFactory);
            var role = await handler.UpdateUserRole(userId, Enums.UserRole.Client);

            Assert.True(role > 0);
        }

        [Fact(Skip = "Not working")]
        public async void GetUserRoleValueForUpdateUser()
        {
           
            const string userId = UserId;

            IHandleAdAuth handler = new AdAuthHandler(Env.Object, HttpHandler, Config, LoggerFactory);
            var role = await handler.UpdateUserRole(userId, Enums.UserRole.Freelancer);

            Assert.True(role > 0);
        }
    }
}
