using System;

namespace Xperters.Admin.UI.Common.GridDefinition
{
    public interface IEnrichableGridView
    {
        IGridDefinitionBuilder GridDefinitionBuilder { get; }

        event EventHandler Load;
    }
}
