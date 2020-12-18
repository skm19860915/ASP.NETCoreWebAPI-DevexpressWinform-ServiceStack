using System.Collections.Generic;

namespace Xperters.Admin.UI.Common.GridDefinition
{
    public abstract class GridElementDefinition<T> : IGridElementDefinition
    {
        protected const string NotSet = "Not Set";
        protected List<GridElementDefinition<T>> _gridElementDefinitions = new List<GridElementDefinition<T>>();
        public IEnumerable<IGridElementDefinition> GridElementDefinitions => _gridElementDefinitions.AsReadOnly();
        public string Tooltip { get; protected set; } = NotSet;
        public string Title { get; protected set; } = NotSet;
        public bool IsVisible { get; protected set; } = true;
        public bool IgnoresMask { get; protected set; } = false;
    }
}