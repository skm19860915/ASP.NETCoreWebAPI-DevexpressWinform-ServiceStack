using System;

namespace Xperters.Admin.UI.Common
{
	public interface IView
	{
		event EventHandler Load;

		event EventHandler HandleDestroyed;

		object Invoke(Delegate @delegate);

		bool InvokeRequired { get; }
	}
}
