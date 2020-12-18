using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Autofac;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Text;
using Xperters.Admin.UI.Common.LayerProgram;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ErrorWarningTab;
using Xperters.Admin.UI.Tabs.JobTab;
using Xperters.Admin.UI.Tabs.JobTab.ServiceClient;
using Xperters.Admin.UI.Tabs.MilestoneAdminApprovals;
using Xperters.Admin.UI.Tabs.MilestoneAdminApprovals.ServiceClient;
using Xperters.Admin.UI.Tabs.PaymentTab;
using Xperters.Admin.UI.Tabs.PaymentTab.ServiceClient;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;
using Xperters.Admin.UI.Tabs.UserTab;
using Xperters.Admin.UI.Tabs.UserTab.ServiceClient;
using Xperters.Authentication.Native;
using Xperters.Core.Logging;

namespace Xperters.Admin.UI.Common
{
	public class GlobalAutofacModule : Module
	{
		private const string XpertersAdminUrlSettingKey = "addin:apiServerBaseAddress";
		private const string EnvironmentNameKey = "xpertersAdmin:environment-name";
		private const string EnvironmentVersionKey = "xpertersAdmin:environment-version";

		protected override async void Load(ContainerBuilder builder)
		{
			JsConfig.DateHandler = DateHandler.ISO8601;
			JsConfig.AlwaysUseUtc = true;
			JsConfig.AssumeUtc = true;
			//ServicePointManager.DefaultConnectionLimit = 100;
			ConfigDatetimeSerialization();

			var azureNativeConfig = new AzureAdNativeOptions(ConfigurationManager.AppSettings["AzureAd:Authority"],
				ConfigurationManager.AppSettings["AzureAd:ClientId"],
				ConfigurationManager.AppSettings["AzureAd:Resource"],
				ConfigurationManager.AppSettings["AzureAd:RedirectUri"],
				ConfigurationManager.AppSettings["xpertersadmin:environment-name"]);

			builder.Register<ILogger>(container => Program.LoggerSingleton.Value);

			//services
			var xpertersAdminUrl = ConfigurationManager.AppSettings[XpertersAdminUrlSettingKey]
							  ?? throw new SettingsPropertyNotFoundException($"{XpertersAdminUrlSettingKey} could not be found");

			var context = SynchronizationContext.Current;
			AuthenticationInfo authenticationInfo = new AuthenticationInfo(azureNativeConfig, Dispatcher.CurrentDispatcher, Program.LoggerSingleton.Value);

			builder.Register(o => authenticationInfo)
				.As<AuthenticationInfo>()
				.SingleInstance();

            builder.RegisterType(typeof(PaymentAdminApprovalPresenter)).AsSelf();

            builder.RegisterType(typeof(JobInformationPresenter)).AsSelf();

			builder.RegisterType(typeof(PaymentWithdrawalsPresenter)).AsSelf();

			builder.RegisterType(typeof(PaymentIncomingPresenter)).AsSelf();

			builder.RegisterType(typeof(UserInfoPresenter)).AsSelf();

			builder.RegisterType(typeof(ErrorWarningPresenter)).AsSelf();

			var environmentName = ConfigurationManager.AppSettings[EnvironmentNameKey] ?? throw new SettingsPropertyNotFoundException($"{EnvironmentNameKey} could not be found");
			var environmentVersion = ConfigurationManager.AppSettings[EnvironmentVersionKey] ?? throw new SettingsPropertyNotFoundException($"{EnvironmentVersionKey} could not be found");

			// https://github.com/msgpack/msgpack-cli/issues/300#issuecomment-418731057
			MsgPack.Serialization.SerializationContext.Default.CompatibilityOptions.PackerCompatibilityOptions &= MsgPack.PackerCompatibilityOptions.ProhibitExtendedTypeObjects;
			MsgPack.Serialization.SerializationContext.Default.DefaultDateTimeConversionMethod = MsgPack.Serialization.DateTimeConversionMethod.Native;

			var jsonServiceClient = new JsonServiceClient(xpertersAdminUrl)
			{
			};

			builder.Register(o => new System.Net.Http.HttpClient())
				.As<System.Net.Http.HttpClient>()
				.SingleInstance();

			builder.Register(o => new XpertersAdminServiceClient(jsonServiceClient, authenticationInfo,
					Program.LoggerSingleton.Value))
				.As<IXpertersAdminServiceClient>()
				.SingleInstance();

			builder.RegisterType<BasicMediator>()
				.As<IMediator>()
				.SingleInstance();

			builder.RegisterType<MilestonesServiceClient>()
				.As<IMilestonesServiceClient>()
				.SingleInstance();

			builder.RegisterType<JobsServiceClient>()
				.As<IJobsServiceClient>()
				.SingleInstance();

			builder.RegisterType<WithdrawalsServiceClient>()
				.As<IWithdrawalsServiceClient>()
				.SingleInstance();

			builder.RegisterType<PaymentsIncomingServiceClient>()
				.As<IPaymentsIncomingServiceClient>()
				.SingleInstance();

			builder.RegisterType<UserServiceClient>()
				.As<IUserServiceClient>()
				.SingleInstance();

			builder.RegisterType<MemoryCacheClient>()
				.As<ICacheClient>()
				.SingleInstance();

            // NOTE : force access token refresh before anything loads
            authenticationInfo.RefreshAsync().GetAwaiter().GetResult();

			ConfigureUserSession(builder, authenticationInfo);

			await Task.CompletedTask;
		}

		private void ConfigureUserSession(ContainerBuilder builder, AuthenticationInfo authenticationInfo)
		{
			var service = new UserSessionClientService(authenticationInfo);

			var userSession = service.GetUserSession();

			builder.Register(o => userSession)
				.SingleInstance();
		}

		private static void ConfigDatetimeSerialization()
		{
			Func<DateTime, string> serializeFn = time => new DateTimeOffset(DateTime.SpecifyKind(time, DateTimeKind.Utc)).ToString("yyyy-MM-ddTHH:mm:sszzz");

			JsConfig<DateTime>.SerializeFn = time => serializeFn(time);

			JsConfig<DateTime?>.SerializeFn = time =>
			{
				if (time.HasValue)
					return serializeFn(time.Value);
				else
					return null;
			};
		}
	}
}
