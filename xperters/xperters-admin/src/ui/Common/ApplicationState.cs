using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xperters.Admin.UI.Tabs.JobTab;
using Xperters.Admin.UI.Tabs.MilestoneAdminApprovals;
using Xperters.Admin.UI.Tabs.PaymentTab;
using Xperters.Admin.UI.Tabs.UserTab;

namespace Xperters.Admin.UI.Common
{
	public static class ApplicationState
	{
		private static readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

		public static PaymentAdminApprovalPresenter PaymentAdminApprovalPresenter { get => Get<PaymentAdminApprovalPresenter>(); set => Set(value); }
        public static JobInformationPresenter JobInformationPresenter { get => Get<JobInformationPresenter>(); set => Set(value); }
		public static PaymentWithdrawalsPresenter PaymentWithdrawalsPresenter { get => Get<PaymentWithdrawalsPresenter>(); set => Set(value); }
		public static PaymentIncomingPresenter PaymentIncomingPresenter { get => Get<PaymentIncomingPresenter>(); set => Set(value); }
		public static UserInfoPresenter userInfoPresenter  { get => Get<UserInfoPresenter>(); set => Set(value); }
		private static T Get<T>([CallerMemberName] string name = null)
		{
			Debug.Assert(name != null, "name != null");
            if (_properties.TryGetValue(name, out var value))
				return value == null ? default(T) : (T)value;
			return default(T);
		}

		private static void Set<T>(T value, [CallerMemberName] string name = null)
		{
			Debug.Assert(name != null, "name != null");
			_properties.AddOrUpdate(name, value, (k, v) => value);
		}
	}
}