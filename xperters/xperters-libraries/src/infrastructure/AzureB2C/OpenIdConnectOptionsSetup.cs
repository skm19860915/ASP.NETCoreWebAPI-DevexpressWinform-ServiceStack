using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using xperters.azuread;
using xperters.business.Interfaces;
using xperters.configurations;
using xperters.configurations.Settings.Ad;
using xperters.constants;
using xperters.domain;
using xperters.domain.Extensions;
using xperters.extensions;
using xperters.infrastructure.Logging;

namespace xperters.infrastructure.AzureB2C
{
    public static class AzureAdB2CAuthenticationBuilderExtensions
    {
        private static readonly ILogger Logger;

        static AzureAdB2CAuthenticationBuilderExtensions()
        {
            Logger = ApplicationLogging.CreateLogger("AzureAdB2CAuthenticationBuilderExtensions");
        }


        public static AuthenticationBuilder AddAzureAdB2C(this AuthenticationBuilder builder, Action<AzureAdB2CSettings> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<OpenIdConnectOptions>, OpenIdConnectOptionsSetup>();
            builder.AddOpenIdConnect();
            Logger.LogDebug("configured Azure B2C");
            return builder;
        }

        public class OpenIdConnectOptionsSetup : IConfigureNamedOptions<OpenIdConnectOptions>
        {
            private ILogger<OpenIdConnectOptionsSetup> _openIdlogger;

            public OpenIdConnectOptionsSetup(IOptions<AzureAdB2CSettings> options)
            {
                AzureAdB2CSettings = options.Value;
            }
            public OpenIdConnectOptionsSetup(AzureAdB2CSettings settings, ILoggerFactory loggerFactory)
            {
                AzureAdB2CSettings = settings;
                _openIdlogger = loggerFactory.CreateLogger<OpenIdConnectOptionsSetup>();
            }

            private AzureAdB2CSettings AzureAdB2CSettings { get; set; }

            public void Configure(string name, OpenIdConnectOptions options)
            {
                options.ClientId = AzureAdB2CSettings.ClientId;
                options.Authority = AzureAdB2CSettings.Authority;
                options.UseTokenLifetime = true;
                options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name" };

                options.Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProvider = OnRedirectToIdentityProvider,
                    OnRemoteFailure = OnRemoteFailure,
                    OnAuthorizationCodeReceived = OnAuthorizationCodeReceived,
                    OnTokenValidated = OnSecurityTokenValidated,
                    OnMessageReceived = OnMessageReceived
                };
            }

            private Task OnMessageReceived(MessageReceivedContext context)
            {
                var serviceProvider = context.HttpContext.RequestServices;
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                var logger = loggerFactory.CreateLogger<OpenIdConnectOptionsSetup>();

                logger.LogDebug($"DEBUG Message received: {context.ProtocolMessage.Display}");
                return Task.FromResult(0);
            }

            private async Task OnSecurityTokenValidated(TokenValidatedContext context)
            {
                var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
                var serviceProvider = context.HttpContext.RequestServices;

                var accountManager = serviceProvider.GetService<IAccountManager>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                _openIdlogger = loggerFactory.CreateLogger<OpenIdConnectOptionsSetup>();

                _openIdlogger.LogDebug("Azure B2C security token validated");
                ProcessValidatedToken(claimsIdentity, accountManager);
            }

            private  void ProcessValidatedToken(ClaimsIdentity claimsIdentity, IAccountManager accountManager)
            {
                try
                {
                    var userId = claimsIdentity.FindFirst(ClaimsConstants.UserIdentifier)?.Value;
                    var displayName = claimsIdentity.Name;
                    var firstName = claimsIdentity.FindFirst("givenName")?.Value;
                    var lastName = claimsIdentity.FindFirst("surName")?.Value;
                    var email = claimsIdentity.FindFirst($"{ClaimsConstants.NamePrefix}emailaddress")?.Value;
                    var mobileNumber = claimsIdentity.FindFirst(ClaimsConstants.MobileNumber)?.Value;
                    var countryCode = claimsIdentity.FindFirst("countryCode")?.Value;

                    _openIdlogger.LogDebug($"User {userId} logged in;");

                    if (userId.IsBlank())
                    {
                        throw new Exception("user not found");
                    }

                    if (mobileNumber.IsBlank())
                    {
                        throw new Exception("mobileNumber not found");
                    }

                    // ensure there's no spaces in the mobile number
                    mobileNumber = mobileNumber.Replace(" ", string.Empty);

                    if (email.IsBlank())
                    {
                        throw new Exception("email address is blank");
                    }

                    var user = new UserDto
                    {
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Id = Guid.Parse(userId),
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        MobilePhone = mobileNumber,
                        DisplayName = displayName,
                        IsEnabled = true,
                        IsActive = true
                    };

                    user.SetDisplayName();

                    CreateUserInDatabase(accountManager, user, countryCode);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    _openIdlogger.LogError($"User {claimsIdentity.Name} display name has not been set;");
                    _openIdlogger.LogError(ex.ToString());
                    throw;
                }
            }

            private void CreateUserInDatabase(IAccountManager accountManager, UserDto user, string countryCode)
            {
                accountManager.CreateUser(user, countryCode);
            }

            public void Configure(OpenIdConnectOptions options)
            {
                Configure(Options.DefaultName, options);
            }

            private Task OnRedirectToIdentityProvider(RedirectContext context)
            {
                var defaultPolicy = AzureAdB2CSettings.DefaultPolicy;

                var config = context.HttpContext.RequestServices.GetService<AppConfig>();

                // replace the url
                if (config != null && config.WebsiteAddress.IsNotBlank()) { 

                    context.ProtocolMessage.RedirectUri = context.ProtocolMessage.RedirectUri.ReplaceHostName(config.WebsiteAddress);
                    Logger.LogDebug($"changed redirect uri host to : {config.WebsiteAddress}");
                }

                if (context.Properties.Items.TryGetValue(AzureAdB2CSettings.PolicyAuthenticationProperty, out var policy) &&
                    !policy.Equals(defaultPolicy))
                {
                    context.ProtocolMessage.Scope = OpenIdConnectScope.OpenIdProfile;
                    context.ProtocolMessage.ResponseType = OpenIdConnectResponseType.IdToken;
                    context.ProtocolMessage.IssuerAddress = context.ProtocolMessage.IssuerAddress.ToLower().Replace(defaultPolicy.ToLower(), policy.ToLower());
                    context.Properties.Items.Remove(AzureAdB2CSettings.PolicyAuthenticationProperty);
                }
                else if (!string.IsNullOrEmpty(AzureAdB2CSettings.ApiUrl))
                {
                    context.ProtocolMessage.Scope += $" offline_access {AzureAdB2CSettings.ApiScopes}";
                    context.ProtocolMessage.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                }
                return Task.FromResult(0);
            }

            private Task OnRemoteFailure(RemoteFailureContext context)
            {
                context.HandleResponse();
                // Handle the error code that Azure AD B2C throws when trying to reset a password from the login page 
                // because password reset is not supported by a "sign-up or sign-in policy"
                if (context.Failure is OpenIdConnectProtocolException && context.Failure.Message.Contains("AADB2C90118"))
                {
                    // If the user clicked the reset password link, redirect to the reset password route
                    context.Response.Redirect("/Session/ResetPassword");
                }
                else if (context.Failure is OpenIdConnectProtocolException && context.Failure.Message.Contains("access_denied"))
                {
                    context.Response.Redirect("/");
                }
                else
                {
                    var message = Regex.Replace(context.Failure.Message, @"[^\u001F-\u007F]+", string.Empty);
                    context.Response.Redirect("/Home/Error?message=" + message);
                }
                return Task.FromResult(0);
            }

            private async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedContext context)
            {
                string signedInUserId = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;

                var app = ConfidentialClientApplicationBuilder.Create(AzureAdB2CSettings.ClientId)
                    .WithB2CAuthority(AzureAdB2CSettings.Authority)
                    .WithRedirectUri(AzureAdB2CSettings.RedirectUri)
                    .WithClientSecret(AzureAdB2CSettings.ClientSecret)
                    .Build();

                new MsalStaticCache(signedInUserId, context.HttpContext).EnablePersistence(app.UserTokenCache);

                try
                {
                    var accounts = await app.GetAccountsAsync();
                    var result = await app.AcquireTokenSilent(AzureAdB2CSettings.ApiScopes.Split(' '), accounts.FirstOrDefault())
                        .ExecuteAsync();
                    context.HandleCodeRedemption(result.AccessToken, result.IdToken);
                }
                catch (MsalServiceException ex)
                {
                    // Case when ex.Message contains:
                    // AADSTS70011 Invalid scope. The scope has to be of the form "https://resourceUrl/.default"
                    // Mitigation: change the scope to be as expected
                    Logger.LogError(ex.Message);
                }
            }
        }
    }
}
