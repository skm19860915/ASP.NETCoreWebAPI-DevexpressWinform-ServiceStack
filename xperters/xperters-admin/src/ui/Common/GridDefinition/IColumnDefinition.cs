using System;
using System.Collections.Generic;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid.Rows;

namespace Xperters.Admin.UI.Common.GridDefinition
{
    public interface IColumnDefinition : IGridElementDefinition
    {
        string PropertyPath { get; }
        Func<RepositoryItem> RepositoryItemFactory { get; }
        IEnumerable<Action<EditorRow>> EditorRowModifiers { get; }
        bool IsReadOnly { get; }
        IEnumerable<string> ContextExclusions { get; }
    }
}