using System;
using System.Threading.Tasks;
using FluentValidation;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ErrorWarningTab;
using Xperters.Admin.UI.Tabs.ErrorWarningTab.Events;

namespace Xperters.Admin.UI.Common.Extensions
{
	public static class ExceptionExtensions
	{
		internal static async Task SendToMediatorAsync(
			this Exception ex,
			IMediator mediator)
		{
			if (ex is AggregateException agEx)
			{
				foreach (Exception innerEx in agEx.InnerExceptions)
					await mediator.SendAsync(new ErrorOccurredEvent(new ErrorWarning(Severity.Error, innerEx.Message, innerEx.ToString()))).ConfigureAwait(false);
			}
			else
			{
				await mediator.SendAsync(new ErrorOccurredEvent(new ErrorWarning(Severity.Error, ex.Message, ex.ToString()))).ConfigureAwait(false);
			}
		}

		internal static async Task SendToMediatorAsync(
			this Exception ex,
			IMediator mediator,
			string message)
		{
			if (ex is AggregateException agEx)
			{
				foreach (Exception innerEx in agEx.InnerExceptions)
					await mediator.SendAsync(new ErrorOccurredEvent(new ErrorWarning(Severity.Error, $"{message}: {innerEx.Message}", innerEx.ToString()))).ConfigureAwait(false);
			}
			else
			{
				await mediator.SendAsync(new ErrorOccurredEvent(new ErrorWarning(Severity.Error, $"{message}: {ex.Message}", ex.ToString()))).ConfigureAwait(false);
			}
		}
	}
}
