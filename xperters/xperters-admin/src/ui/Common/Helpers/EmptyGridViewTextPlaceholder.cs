using System;
using System.Drawing;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Base;

namespace Xperters.Admin.UI.Common.Helpers
{
	public class EmptyGridViewTextPlaceholder
	{
		public ColumnView View { get; set; }
		public Func<string> GetText { get; set; }

		public EmptyGridViewTextPlaceholder(ColumnView view, Func<string> getText)
		{
			if (view == null) throw new ArgumentNullException(nameof(view));

			InitTextBounds(view, getText);
		}

		public EmptyGridViewTextPlaceholder(ColumnView view, Func<string> getText, Action onClickAction):this(view, getText)
		{	
			if (view == null) throw new ArgumentNullException(nameof(view));
			if (onClickAction == null) throw new ArgumentNullException(nameof(onClickAction));

			View.MouseUp += (sender, args) =>
			{
				if (Bounds == null)
					return;

				if (Bounds.Value.Contains(args.Location))
				{
					onClickAction();
				}
			};
		}
		
		public EmptyGridViewTextPlaceholder(ColumnView view, Func<string> getText, Func<Task> onClickTask) : this(view, getText)
		{
			if (view == null) throw new ArgumentNullException(nameof(view));
			if (onClickTask == null) throw new ArgumentNullException(nameof(onClickTask));

			View.MouseUp += async (sender, args) =>
			{
				if (Bounds == null)
					return;

				if (Bounds.Value.Contains(args.Location))
				{
					await onClickTask().ConfigureAwait(false);
				}
			};
		}

		private void InitTextBounds(ColumnView view, Func<string> getText)
		{
			var drawFormat = new StringFormat();
			drawFormat.Alignment = drawFormat.LineAlignment = StringAlignment.Center;

			View = view;
			GetText = getText;

			View.CustomDrawEmptyForeground += (sender, e) =>
			{
				if (GetText != null)
				{
					Bounds = new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
					e.Graphics.DrawString(GetText(), e.Appearance.Font, SystemBrushes.ControlDark, Bounds.Value, drawFormat);
				}
			};
		}

		private RectangleF? Bounds { get; set; }
	}
}
