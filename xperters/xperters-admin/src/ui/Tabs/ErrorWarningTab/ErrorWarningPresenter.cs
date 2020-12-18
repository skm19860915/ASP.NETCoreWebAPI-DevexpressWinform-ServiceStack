using System;
using System.Reactive;
using System.Threading.Tasks;
using FluentValidation;
using ReactiveUI;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ErrorWarningTab.Events;

namespace Xperters.Admin.UI.Tabs.ErrorWarningTab
{
	public class ErrorWarningPresenter
	{
		private IErrorWarningView ErrorWarningView { get; }
		public ReactiveCommand<Unit, Unit> UploadFileCommand { get; }
		private IMediator Mediator { get; }
		public ReactiveCommand<Unit, Unit> ClearErrorsCommand { get; }

		public ErrorWarningPresenter(
			IErrorWarningView view
			, IMediator mediator)
		{
			ErrorWarningView = view ?? throw new ArgumentNullException(nameof(view));
			Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			ClearErrorsCommand = ReactiveCommand.CreateFromTask<Unit>(ExecuteClearErrorsCommand);
			Mediator.Register<ErrorOccurredEvent>(HandleErrorOccurredEvent, this);
			view.HandleDestroyed += ViewClosing;
			view.Load += ViewLoad;
		}

		private async Task<Unit> ExecuteClearErrorsCommand()
		{
			ErrorWarningView.SafelyUpdateUi(() =>
			{
				ErrorWarningView.ErrorsWarnings.Clear();
			});

			return await Task.FromResult(Unit.Default);
		}

		private async Task HandleErrorOccurredEvent(ErrorOccurredEvent arg)
		{
			// We only want to show errors in the error / warnings grid, not warnings.
			if (arg.ErrorWarning.Severity == Severity.Error)
			{
				ErrorWarningView.SafelyUpdateUi(() =>
				{
					ErrorWarningView.ErrorsWarnings.Insert(0, arg.ErrorWarning);
					ErrorWarningView.MakeVisible();
				});
			}
			await Task.CompletedTask;
		}

		private void ViewClosing(object sender, EventArgs e)
		{
			Mediator.DeregisterAll(this);
		}

		private async void ViewLoad(object sender, EventArgs e)
		{
			await Task.CompletedTask;
		}
	}
}
