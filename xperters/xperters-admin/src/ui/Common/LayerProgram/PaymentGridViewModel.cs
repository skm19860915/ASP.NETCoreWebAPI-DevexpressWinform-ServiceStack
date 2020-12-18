using System;
using xperters.domain;

namespace Xperters.Admin.UI.Common.LayerProgram
{
	public class PaymentGridViewModel : RootViewModel<PaymentGridViewModel>
	{
		public MilestonePaymentDto Payment { get; set; }
		private bool _isDirty = false;
		internal override bool IsDirty => _isDirty;

		public PaymentGridViewModel(MilestonePaymentDto payment)
		{
			Payment = payment ?? throw new ArgumentNullException(nameof(payment));
		}

		internal void MakeDirty()
		{
			_isDirty = true;
		}

		internal void Clean()
		{
			IsSaving = false;
			_isDirty = false;
		}

		public bool IsSaving { get; set; }
	}
}