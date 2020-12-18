using System.Collections.Generic;

namespace Xperters.Admin.UI.Common.GridDefinition
{
    public interface IGridDefinitionBuilder
    {
        IColumnDefinition GetColumnDefinition(string propertyPath);

        IEnumerable<IColumnDefinition> GetAllColumnDefinitions();

        string PropertyPathPrefix { get; }

        bool HasColumnDefinition(string propertyPath);

        IColumnGroupDefinition GetAnchorGroup(string context);

        void Compile();
    }
}