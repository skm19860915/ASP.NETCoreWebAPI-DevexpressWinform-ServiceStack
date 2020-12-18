using Xperters.Admin.UI.Common.Mediator;

namespace Xperters.Admin.UI.Tabs.ErrorWarningTab.Events
{
	public class ErrorOccurredEvent : IEvent
	{
		public ErrorWarning ErrorWarning { get; }

		public ErrorOccurredEvent(ErrorWarning errorWarning)
		{
			ErrorWarning = errorWarning;
		}
	}
}
