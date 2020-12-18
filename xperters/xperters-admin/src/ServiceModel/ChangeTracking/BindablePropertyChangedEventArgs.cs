using System;

namespace Xperters.Admin.ServiceModel.ChangeTracking
{
	public class BindablePropertyChangedEventArgs : EventArgs
	{
		public BindablePropertyChangedEventArgs(
			object oldValue,
			object newValue,
			string propertyName,
			BindablePropertyChangedReason changeReason)
		{
			OldValue = oldValue;
			NewValue = newValue;
			PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
			ChangeReason = changeReason;
		}

		public string PropertyName { get; }
		public BindablePropertyChangedReason ChangeReason { get; }
		public object OldValue { get; }
		public object NewValue { get; }
	}
}
