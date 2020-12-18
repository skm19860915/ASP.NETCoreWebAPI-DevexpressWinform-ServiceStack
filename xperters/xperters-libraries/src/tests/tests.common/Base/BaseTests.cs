using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using xperters.configurations;
using xperters.configurations.Settings.Ad;
using xperters.constants;
using xperters.domain;
using xperters.mockdata;

namespace xperters.tests.common.Base
{
    public abstract class BaseTests
    {
        protected readonly string ContentDisposition;
        protected readonly AppConfig AppConfig;
        protected const string FileName = "appsettings.json";
        protected const string ContentType = "application/json";

        protected static IMapper Mapper { get; private set; }
        protected ILoggerFactory  LoggerFactory { get; private set; }

        protected BaseTests()
        {
            LoggerFactory = new LoggerFactory();

            var appConfig = new Mock<IOptions<AppConfig>>();

            appConfig.Setup(x => x.Value).Returns(new AppConfig
            {
                DatabaseConnectionString = AppSettings.DatabaseConnectionStringInMemory,
                Storage = new configurations.Settings.StorageAccountSettings
                {
                    BaseUrl = AppSettings.StorageBaseUrlDeveloper,
                    ConnectionString = AppSettings.StorageAccountDeveloper
                },
                AzureAdB2CSettings = new AzureAdB2CSettings
                {
                    ClientId = "c8d3d19e-9e4f-4eda-91fd-19e33dd41f40",
                    Tenant = "fd4eaeb4-1c75-4f9b-b358-a78c10b4f9d6",
                    GraphUri = "ClientId",
                    GraphRelativePath = "ClientId",
                    Domain = "ClientId",
                    SignUpSignInPolicyId = "B2C_1A_SignUpOrSignInWithPhone",
                    SignInPolicyId = "B2C_1A_SignUpOrSignInWithPhone",
                    SignUpPolicyId = "B2C_1A_SignUpOrSignInWithPhone",
                    ResetPasswordPolicyId = "b2c_1_reset",
                    EditProfilePolicyId = "b2c_1_edit_profile",
                    RedirectUri = "https://localhost:44387/signin-oidc",
                    ClientSecret = "shy15XB/.f:KlZ0Iy2_rlChv",
                    ApiUrl = "https://graph.microsoft.com/v1.0/users",
                    ApiScopes = "https://graph.microsoft.com/.default",
                }
            });

            AppConfig = appConfig.Object.Value;
            AutoMapperConfig.Initialize(appConfig.Object, LoggerFactory);

            Mapper = AutoMapperConfig.InitializedMapper;
            MockDataFactory.Create();

            ContentDisposition = $"inline; filename={FileName}";
        }

        protected Mock<IHttpContextAccessor> CreateHttpContext(string displayName, UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, displayName),
                new Claim(ClaimsConstants.UserIdentifier, user.Id.ToString()),
                new Claim("name", displayName)
            };

            var identity = new ClaimsIdentity(claims, "Test");
            var signedInPrincipal = new ClaimsPrincipal(identity);

            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var defaultHttpContext = new DefaultHttpContext { User = signedInPrincipal };

            httpContextAccessor.SetupGet(x => x.HttpContext).Returns(defaultHttpContext);

            return httpContextAccessor;
        }
    }
}
