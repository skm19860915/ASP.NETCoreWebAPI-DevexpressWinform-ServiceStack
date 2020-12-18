using System;
using System.Windows.Forms;

namespace Xperters.Admin.UI.Common.Extensions
{
	public static class IViewExtensions
	{
		public static void SafelyUpdateUi(
			this IView view,
			Action action)
		{
			if (view == null)
				throw new ArgumentNullException(nameof(view));
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			if (view.InvokeRequired)
			{
				MethodInvoker invoker = new MethodInvoker(delegate
				{
					action.Invoke();
				});

				view.Invoke(invoker);
			}
			else
				action();
		}

		public static void SafelyUpdateControl(
			this Control view,
			Action action)
		{
			if (view == null)
				throw new ArgumentNullException(nameof(view));
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			if (view.InvokeRequired)
			{
				MethodInvoker invoker = new MethodInvoker(delegate
				{
					action.Invoke();
				});

				view.Invoke(invoker);
			}
			else
				action();
		}
	}
}
