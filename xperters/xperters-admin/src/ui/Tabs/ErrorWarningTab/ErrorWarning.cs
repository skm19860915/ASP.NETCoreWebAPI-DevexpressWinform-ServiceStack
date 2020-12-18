using System;
using System.Threading.Tasks;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.ErrorWarningTab
{
	public class ErrorWarning
	{
		public FluentValidation.Severity Severity { get; }
		public string ErrorMessage { get; }
		public string ErrorDescription { get; }
		public Func<Task> InvokeAction { get; }
		public Enums.ErrorWarningSource Source { get; }
		public DateTime DateTimeofError { get; }

		internal ErrorWarning(
			FluentValidation.Severity severity,
			string errorMessage,
			string errorDescription)
		{
			Severity = severity;
			ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
			ErrorDescription = errorDescription ?? throw new ArgumentNullException(nameof(errorDescription));
			DateTimeofError = DateTime.Now;
		}

		internal ErrorWarning(
			FluentValidation.Severity severity,
			string errorMessage,
			string errorDescription,
			Func<Task> invokeAction) : this(severity, errorMessage, errorDescription)
		{
			InvokeAction = invokeAction;
		}

		internal ErrorWarning(
			Enums.ErrorWarningSource source,
			FluentValidation.Severity severity,
			string errorMessage,
			string errorDescription,
			Func<Task> invokeAction) : this(severity, errorMessage, errorDescription, invokeAction)
		{
			Source = source;
		}
	}
}
