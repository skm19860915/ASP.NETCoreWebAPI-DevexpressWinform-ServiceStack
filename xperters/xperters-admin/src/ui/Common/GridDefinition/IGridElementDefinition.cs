using System.Collections.Generic;

namespace Xperters.Admin.UI.Common.GridDefinition
{
    public interface IGridElementDefinition
    {
        string Tooltip { get; }
        string Title { get; }
        bool IsVisible { get; }
        bool IgnoresMask { get; }
        IEnumerable<IGridElementDefinition> GridElementDefinitions { get; }
    }
}