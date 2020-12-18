using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.GridDefinition;

namespace Xperters.Admin.UI
{
    public static class IGridDefinitionBuilderExtensions
    {
	    public static void Enrich(this IEnrichableGridView enrichableGridView, VGridControl vGridControl, string context)
	    {
		    vGridControl.Rows.Clear();

		    vGridControl.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
		    var topGroup = enrichableGridView.GridDefinitionBuilder.GetAnchorGroup(context);
		    GenerateColumns(enrichableGridView.GridDefinitionBuilder
			    , o => vGridControl.Rows.Add(o)
			    , topGroup.GridElementDefinitions
			    , vGridControl.RepositoryItems
			    , context);

		    PostBind(enrichableGridView, vGridControl);
	    }

	    private static void PostBind(IEnrichableGridView enrichableGridView, VGridControl vGridControl)
	    {
		    enrichableGridView.Load += (sender, e) =>
		    {
			    foreach (var row in vGridControl.GetAllRows())
			    {
				    if (row.Properties.RowEdit?.Tag is Action action)
					    action();
			    }
		    };
	    }

	    public static void EnrichToolTip(this IEnrichableGridView enrichableGridView, VGridControl vGridControl)
	    {


	    }

	    private static void GenerateColumns(
		    IGridDefinitionBuilder gridDefinitionBuilder,
		    Action<BaseRow> addToParent,
		    IEnumerable<IGridElementDefinition> elements,
		    RepositoryItemCollection repositoryItems,
		    string context)
	    {
		    foreach (var element in elements)
		    {
			    if (element is IColumnDefinition columnDefinition)
			    {
				    if (!columnDefinition.ContextExclusions.Contains(context))
				    {
					    EditorRow editorRow = new EditorRow();
					    editorRow.Tag = new GridColumnTagBag
					    {
						    GridElementDefinition = element
					    };

					    editorRow.Properties.FieldName = columnDefinition.PropertyPath;

					    if (!string.IsNullOrWhiteSpace(gridDefinitionBuilder.PropertyPathPrefix))
						    editorRow.Properties.FieldName = editorRow.Properties.FieldName.Replace(FormattableString.Invariant($"{gridDefinitionBuilder.PropertyPathPrefix}."), string.Empty);

					    editorRow.Properties.Caption = columnDefinition.Title;
					    editorRow.Properties.ToolTip = columnDefinition.Tooltip;
					    editorRow.Properties.RowEdit = columnDefinition.RepositoryItemFactory();
					    if (!repositoryItems.Contains(editorRow.Properties.RowEdit))
						    repositoryItems.Add(editorRow.Properties.RowEdit);
					    editorRow.Properties.ReadOnly = columnDefinition.IsReadOnly;
					    editorRow.Visible = element.IsVisible;
					    foreach (var editorRowModifier in columnDefinition.EditorRowModifiers)
					    {
						    editorRowModifier(editorRow);
					    }

					    addToParent(editorRow);
					    GenerateColumns(gridDefinitionBuilder, o => editorRow.ChildRows.Add(o), element.GridElementDefinitions, repositoryItems, context);
				    }
			    }
			    else
			    {
				    CategoryRow categoryRow = new CategoryRow();
				    categoryRow.Tag = new GridColumnTagBag
				    {
					    GridElementDefinition = element
				    };
				    categoryRow.Properties.Caption = element.Title;
				    categoryRow.Properties.ToolTip = element.Tooltip;
				    categoryRow.Visible = element.IsVisible;
				    addToParent(categoryRow);
				    GenerateColumns(gridDefinitionBuilder, o => categoryRow.ChildRows.Add(o), element.GridElementDefinitions, repositoryItems, context);
			    }
		    }
	    }

	    public static void Enrich(this IEnrichableGridView enrichableGridView, GridView gridView)
	    {


	    }
    }
}
