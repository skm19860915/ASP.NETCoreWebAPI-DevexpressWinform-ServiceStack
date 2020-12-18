using System;
using System.Windows.Forms;

namespace Xperters.Admin.UI.Common.Helpers
{
	public static class ClipboardHelper
	{
		public static void AddHtmlToClipboard(string html)
		{
			const string contextStart = "<HTML><BODY><!--StartFragment -->";
			const string contextEnd = "<!--EndFragment --></BODY></HTML>";
			const string startHtml = "<#STARTA_#>";
			const string endHtml = "<#_ENDA#>";
			const string startFragment = "<#STARTB_#>";
			const string endFragment = "<#_ENDB#>";
			var headerDescription =
				$"Version:1.0{Environment.NewLine}StartHTML:{startHtml}{Environment.NewLine}EndHTML:{endHtml}{Environment.NewLine}StartFragment:{startFragment}{Environment.NewLine}EndFragment:{endFragment}{Environment.NewLine}";

			var clipboardData = headerDescription + contextStart + html + contextEnd;
			clipboardData = clipboardData
				.Replace(startHtml, headerDescription.Length.ToString().PadLeft(10, '0'))
				.Replace(endHtml, clipboardData.Length.ToString().PadLeft(10, '0'))
				.Replace(startFragment, (headerDescription + contextStart).Length.ToString().PadLeft(10, '0'))
				.Replace(endFragment, (headerDescription + contextStart + html).Length.ToString().PadLeft(10, '0'));

			Clipboard.SetDataObject(new DataObject(DataFormats.Html, clipboardData), true, 3, 500);
		}

		public static void Paste()
		{
			SendKeys.SendWait("^v");
		}
	}
}
