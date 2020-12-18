using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using xperters.configurations;
using xperters.constants;
using xperters.domain;
using xperters.entities.Extensions;
using xperters.http;

namespace xperters.integration.tests
{
    public abstract class BaseTests
    {
        protected readonly LoggerFactory LoggerFactory;
        protected readonly AppConfig Config;
        protected readonly HttpHandler HttpHandler;
        protected readonly Mock<IHostEnvironment> Env;
        protected const string UserAdId = UserId;
        protected const string UserId = "291e0127-28e2-4af5-ada6-bd4d2db7e501";

        protected BaseTests()
        {
            LoggerFactory = new LoggerFactory();
            var path = Directory.GetCurrentDirectory();
            Env = new Mock<IHostEnvironment>();
            Env.SetupGet(x => x.ContentRootPath).Returns(path);

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependenciesStandAlone(Env.Object);
            var appConfigBuilder = new AppConfigBuilder(Env.Object, serviceCollection);
            Config = appConfigBuilder.Build();

            HttpHandler = new HttpHandler(LoggerFactory);

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
