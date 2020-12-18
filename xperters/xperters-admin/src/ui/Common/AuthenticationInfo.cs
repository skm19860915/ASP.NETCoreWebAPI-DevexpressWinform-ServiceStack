using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using ServiceStack;
using Xperters.Authentication.Native;
using AuthenticationResult = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult;

namespace Xperters.Admin.UI.Common
{
	public class AuthenticationInfo
	{
		public AuthenticationResult AuthenticationResult { get; set; }
		private SemaphoreSlim SemaphoreSlim { get; } = new SemaphoreSlim(1);
        private readonly AzureAdNativeOptions _azureAdNativeOptions;
        private readonly Dispatcher _dispatcher;
        private readonly Core.Logging.ILogger _logger;

		public AuthenticationInfo(AzureAdNativeOptions azureAdNativeOptions, Dispatcher dispatcher, Core.Logging.ILogger logger)
		{
			_dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
			_azureAdNativeOptions = azureAdNativeOptions ?? throw new ArgumentNullException(nameof(azureAdNativeOptions));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public string DisplayName => AuthenticationResult == null ? "Unknown" : $"{AuthenticationResult.UserInfo.GivenName} {AuthenticationResult.UserInfo.FamilyName}";

        private async Task RefreshToken()
		{
			//https://stackoverflow.com/questions/303116/system-windows-threading-dispatcher-and-winforms
			try
			{
				if (_dispatcher.Thread == Thread.CurrentThread)
					AuthenticationResult = await AzureAuthentication.AuthenticateNativeClientAuthenticationResult(_azureAdNativeOptions).ConfigureAwait(false);
				else
					await _dispatcher.InvokeAsync(async () =>
					{
						AuthenticationResult = await AzureAuthentication.AuthenticateNativeClientAuthenticationResult(_azureAdNativeOptions).ConfigureAwait(false);
					}, DispatcherPriority.Background);
			}
			catch (CryptographicException e) when (e.Message.Contains("The data is invalid"))
			{
				TryClearLocalTokenCache();
			}
			catch (TokenException)
			{
				TryClearLocalTokenCache();
			}
		}

		private void TryClearLocalTokenCache()
		{
			string dirPath = null;
			try
			{
				dirPath = $"{Environment.GetEnvironmentVariable("LocalAppData")}\\Xperters.Admin.UI_{_azureAdNativeOptions.Env}";
				File.Delete($"{dirPath}\\TokenCache.dat");
				MessageBox.Show(
					"Xperters Admin repaired a problem with your token. Please restart Xperters Admin for the changes to take affect",
					"Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Application.Exit();
			}
			catch (Exception e)
			{
				MessageBox.Show(
					$"We tried to clear your Xperters Admin token but failed due to : {e.Message}. Please try to manually delete 'TokenCache.dat' from the path '{dirPath}'");

				if (!string.IsNullOrWhiteSpace(dirPath))
					Process.Start(dirPath);
			}
		}

		private bool TokenHasExpired => (AuthenticationResult?.ExpiresOn.UtcDateTime).GetValueOrDefault(DateTime.MinValue) < DateTime.UtcNow.AddMinutes(10);

        public async Task RefreshAsync(bool force = false)
		{
			//Normal lock does not work with async
			if (!await SemaphoreSlim.WaitAsync(15000).ConfigureAwait(false))
				_logger.Error("Timeout: Could not obtain RefreshAsync semaphore.");

			try
			{
				if (TokenHasExpired || force)
					await RefreshToken().ConfigureAwait(false);
			}
			finally
			{
				SemaphoreSlim.Release();
			}

		}
	}
}