using System.Drawing;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Xperters.Admin.UI")]

namespace Xperters.Admin.UI
{
	internal static class Constants
	{
		internal static class LaunchDarklyKeys
		{
			internal const string TranslatorIntegrationEnabled = "translator-integration-enabled";
		}

		internal static class Colors
		{
			internal static readonly Color GenesisRecordColor = Color.FromArgb(238, 220, 244);
			internal static readonly Color PrimaryForeColor = Color.FromArgb(170, 79, 196);

			internal static readonly Color GridDirtyRecordForegroundColor = Color.FromArgb(134, 51, 157);

			internal static readonly Color GridDirtyCellBackgroundColor = Color.FromArgb(172, 79, 198);
			internal static readonly Color GridDirtyCellForegroundColor = Color.White;

			internal static readonly Color GridSelectedRecordBackgroundColor = Color.FromArgb(238, 220, 244);
			internal static readonly Color GridFocusedRecordBackgroundColor = Color.FromArgb(230, 240, 255);
			internal static readonly Color GridLayerSelectionBackgroundColor = Color.FromArgb(230, 240, 255);
			internal static readonly Color GridErrorCellBackgroundColor = Color.FromArgb(255, 102, 102);
			internal static readonly Color GridWarningCellBackgroundColor = Color.FromArgb(255, 204, 0);
			internal static readonly Color GridFilterCellBackgroundColor = Color.FromArgb(189, 113, 211);
			internal static readonly Color GridFilterCellBackgroundColorBright = Color.Yellow;
			internal static readonly Color GridDisabledCellBackgroundColor = Color.FromArgb(240, 240, 240);
			internal static readonly Color CurrentRecordColor = Color.FromArgb(238, 220, 244);
			internal static readonly Color ReadOnlyCellForegroundColor = Color.Gray;
			internal static readonly Color ReadOnlyCellBackgroundColor = Color.White;
			internal static readonly Color CellLoadingForegroundColor = Color.LightSkyBlue;
			internal static readonly Color CellErrorForegroundColor = Color.Red;
			internal static readonly Color VGridFocusedHeaderBackColor = GridFocusedRecordBackgroundColor;
			internal static readonly Color VGridFocusedHeaderForeColor = Color.Black;
		}


		internal static class ColumnDefinitionContexts
		{
			internal const string Layer = "Layer";
			internal const string Leg = "Leg";
			internal const string Trigger = "Trigger";
			internal const string Tac = "Tac";
			internal const string Counterparty = "Counterparty";
			internal const string SubjectLoss = "SubjectLoss";
		}

		internal static class Messages
		{
			internal const string OptimisticConcurrencyExceptionFriendlyMessage = "The data you tried to save is out of date. Refresh your local data and try again. This happened because the data was modified by another user or by a backend process.";
		}

		internal const string CaptureResourceLayoutPath = "Layouts/Capture.xml";
		internal const string AnalysisResourceLayoutPath = "Layouts/Analysis.xml";
		internal const string ReportingResourceLayoutPath = "Layouts/Reporting.xml";
		internal const string SignOffResourceLayoutPath = "Layouts/SignOff.xml";
		internal const int IgnitionApiVersion = 2;

		internal static class ProgramDocumentFolderNames
		{
			internal const string ModelingInfo = "MODELLING INFO";
			internal const string ReMetrica = "ReMetrica";
		}
	}
}
