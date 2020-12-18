using System.ComponentModel;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.ErrorWarningTab
{
	public interface IErrorWarningView : IView
	{
		BindingList<ErrorWarning> ErrorsWarnings { get; }
		void MakeVisible();

	}
}
