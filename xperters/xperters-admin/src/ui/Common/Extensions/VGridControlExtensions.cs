using System.Collections.Generic;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;

namespace Xperters.Admin.UI.Common.Extensions
{
    public static class VGridControlExtensions
    {
        public static IEnumerable<BaseRow> GetAllRows(this VGridControl verticalGrid)
        {
            return GetAllRows(verticalGrid.Rows);
        }

        private static IEnumerable<BaseRow> GetAllRows(IEnumerable<BaseRow> rows)
        {
            foreach (var row in rows)
            {
                yield return row;
                foreach (var childRow in GetAllRows(row.ChildRows))
                {
                    yield return childRow;
                }
            }
        }

        public static void AdjustMeasurements(this VGridControl verticalGrid, int width, int height)
        {
            verticalGrid.RowHeaderWidth = width;
            verticalGrid.RecordWidth = width;
            verticalGrid.GetAllRows().ForEach(o => o.Height = height);
        }
    }
}