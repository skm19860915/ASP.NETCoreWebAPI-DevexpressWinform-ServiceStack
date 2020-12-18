using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Polly;
using Polly.Caching.MemoryCache;
using Polly.Retry;
using Polly.Wrap;
using ServiceStack;
using Xperters.Admin.ServiceModel;
using Xperters.Core.Logging;
using ServiceClientBase = ServiceStack.ServiceClientBase;

namespace Xperters.Admin.UI.Common
{
	public interface IXpertersAdminServiceClient
	{
		Task<TResponse> PostAsync<TResponse>(IReturn<TResponse> requestDto);

		Task<TResponse> GetAsync<TResponse>(IReturn<TResponse> requestDto);

		void Invalidate<TRequest>() where TRequest : IReturn;
	}
	public sealed class XpertersAdminServiceClient : IXpertersAdminServiceClient
	{
		private ServiceClientBase ServiceClient { get; }
		public AuthenticationInfo AuthenticationInfo { get; }

		private RetryPolicy RetryAsyncPolicy = Policy
			.Handle<WebServiceException>(o => o.StatusCode != 401 || o.Message.ToUpperInvariant().Contains("EXPIRED"))
			.WaitAndRetryAsync(3, i => TimeSpan.FromMilliseconds(500));

		private MemoryCacheProvider MemoryCacheProvider { get; } = new MemoryCacheProvider(MemoryCache.Default);
		private PolicyWrap LongTtlAsyncPolicy { get; }

		private PolicyWrap MediumTtlAsyncPolicy { get; }

		private TimeSpan LongTimeToLive { get; } = TimeSpan.FromDays(1);
		private TimeSpan MediumTimeToLive { get; } = TimeSpan.FromMinutes(10);

		private ILogger Logger { get; }

		public XpertersAdminServiceClient(ServiceClientBase client, AuthenticationInfo authenticationInfo, ILogger logger)
		{
			ServiceClient = client ?? throw new ArgumentNullException(nameof(client));
			ServiceClient.Timeout = TimeSpan.FromMinutes(10);
			AuthenticationInfo = authenticationInfo ?? throw new ArgumentNullException(nameof(authenticationInfo));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			var longTtlAsyncCachePolicy = Policy.CacheAsync(MemoryCacheProvider, LongTimeToLive);
			LongTtlAsyncPolicy = Policy.WrapAsync(longTtlAsyncCachePolicy, RetryAsyncPolicy);

			var mediumTtlAsyncCachePolicy = Policy.CacheAsync(MemoryCacheProvider, MediumTimeToLive);
			MediumTtlAsyncPolicy = Policy.WrapAsync(mediumTtlAsyncCachePolicy, RetryAsyncPolicy);

		}


		public async Task<TResponse> PostAsync<TResponse>(
			IReturn<TResponse> requestDto)
		{
			if (requestDto == null)
				throw new ArgumentNullException(nameof(requestDto));

			EnrichWithVersion(requestDto);
			LogInformation(requestDto);


			return await RetryAsyncPolicy.ExecuteAsync(async () =>
			{
				await RefreshIfExpiredTokenAsync().ConfigureAwait(false);
				return await TryPostAsync(requestDto).ConfigureAwait(false);

			}).ConfigureAwait(false);
		}

		public async Task<TResponse> GetAsync<TResponse>(IReturn<TResponse> requestDto)
		{
			if (requestDto == null)
				throw new ArgumentNullException(nameof(requestDto));

			EnrichWithVersion(requestDto);
			LogInformation(requestDto);

			return await RetryAsyncPolicy.ExecuteAsync(async () =>
			{
				await RefreshIfExpiredTokenAsync().ConfigureAwait(false);
				return await TryGetAsync(requestDto).ConfigureAwait(false);

			}).ConfigureAwait(false);
		}

		private void EnrichWithVersion<TResponse>(IReturn<TResponse> requestDto)
		{
			if (requestDto is IHasVersion hasVersion)
				if (hasVersion.Version == 0)
					hasVersion.Version = SecurityConstants.XpertersApiServiceModelVersion;
		}

		private void LogInformation(object request)
		{
			var args = new List<object> { DateTime.UtcNow };

			string log;
			if (request != null)
			{
				log = "{Date}, {RequestType}, {UserDisplayName} ({UserDisplayableId} | {UserUniqueId}";
				args.Add(request);
			}
			else
			{
				log = "{Date}, {UserDisplayName} ({UserDisplayableId} | {UserUniqueId}";
			}

			args.Add(AuthenticationInfo.DisplayName);
			args.Add(AuthenticationInfo.AuthenticationResult?.UserInfo?.DisplayableId);
			args.Add(AuthenticationInfo.AuthenticationResult?.UserInfo?.UniqueId);
			args.Add(AuthenticationInfo.DisplayName);

			Logger.Information(log, args.ToArray());
		}
		private async Task RefreshIfExpiredTokenAsync()
		{
			await AuthenticationInfo.RefreshAsync().ConfigureAwait(false);
			ServiceClient.BearerToken = AuthenticationInfo.AuthenticationResult.AccessToken;
		}

		private async Task<TResponse> TryPostAsync<TResponse>(
			IReturn<TResponse> requestDto,
			params UploadFile[] files)
		{
			try
            {
                if (files != null && files.Any())
				{
					return ServiceClient.PostFilesWithRequest<TResponse>(requestDto, files.AsEnumerable());
				}

                return await ServiceClient.PostAsync(requestDto).ConfigureAwait(false);
            }
			catch (WebServiceException ex) when (ex.Message.ToUpper().Contains("TOKEN HAS EXPIRED"))
			{
				await ForceRefreshTokenAsync().ConfigureAwait(false);
				throw new WebServiceException($"{ex.Message}. Additional token information : ExpiresOn : {AuthenticationInfo?.AuthenticationResult?.ExpiresOn} , Now : {DateTime.UtcNow}", ex);
			}
		}

		private async Task<TResponse> TryGetAsync<TResponse>(IReturn<TResponse> requestDto)
		{
			try
			{
				return await ServiceClient.GetAsync(requestDto).ConfigureAwait(false);
			}
			catch (WebServiceException ex) when (ex.Message.ToUpper().Contains("TOKEN HAS EXPIRED"))
			{
				await ForceRefreshTokenAsync().ConfigureAwait(false);
				throw new WebServiceException($"{ex.Message}. Additional token information : ExpiresOn : {AuthenticationInfo?.AuthenticationResult?.ExpiresOn} , Now : {DateTime.UtcNow}", ex);
			}
		}

		private async Task ForceRefreshTokenAsync()
		{
			await AuthenticationInfo.RefreshAsync(true).ConfigureAwait(false);
			ServiceClient.BearerToken = AuthenticationInfo.AuthenticationResult.AccessToken;
		}

		public void Invalidate<TRequest>() where TRequest : IReturn
		{
			var toRemove = MemoryCache.Default.Where(o => o.Key.Contains(typeof(TRequest).FullName)).ToList();

			foreach (var item in toRemove)
			{
				MemoryCache.Default.Remove(item.Key);
			}
		}
	}
}
