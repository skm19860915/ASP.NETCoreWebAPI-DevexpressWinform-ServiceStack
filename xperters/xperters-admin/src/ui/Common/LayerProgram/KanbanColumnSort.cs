using System;
using DevExpress.Data;
using DevExpress.XtraGrid;

namespace Xperters.Admin.UI.Common.LayerProgram
{
    public class KanbanColumnSort
    {
        public KanbanColumnSort(string fieldName, ColumnSortMode sortMode, ColumnSortOrder sortOrder)
        {
            FieldName = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
            SortMode = sortMode;
            SortOrder = sortOrder;
        }

        public string FieldName { get; set; }
        public ColumnSortMode SortMode { get; set; }
        public ColumnSortOrder SortOrder { get; set; }
    }
}