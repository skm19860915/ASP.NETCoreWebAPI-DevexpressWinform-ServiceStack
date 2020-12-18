using System.Collections.Generic;

namespace Xperters.Admin.UI.Common.GridDefinition
{
    public interface IColumnGroupDefinition : IGridElementDefinition
    {
        IEnumerable<string> Contexts { get; }
    }
}