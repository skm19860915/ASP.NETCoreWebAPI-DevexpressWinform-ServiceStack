using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperters.Admin.UI.Common.GridDefinition
{
    public sealed class ColumnGroupDefinition<T> : GridElementDefinition<T>, IColumnGroupDefinition
    {
        private List<string> _contexts = new List<string>();
        public IEnumerable<string> Contexts { get { return _contexts.AsReadOnly(); } }

        public ColumnGroupDefinition(params string[] contexts)
        {
            _contexts.AddRange(contexts ?? new string[] { });
        }

        public ColumnGroupDefinition<T> WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public ColumnGroupDefinition<T> WithTooltip(string tooltip)
        {
            Tooltip = tooltip;
            return this;
        }

        public ColumnGroupDefinition<T> WithColumnGroup(params string[] contexts)
        {
            var columnGroupDefinition = new ColumnGroupDefinition<T>(contexts);
            _gridElementDefinitions.Add(columnGroupDefinition);

            return columnGroupDefinition;
        }

        public ColumnDefinition<T> WithColumn()
        {
            var columnDefinition = new ColumnDefinition<T>();
            _gridElementDefinitions.Add(columnDefinition);

            return columnDefinition;
        }
    }
}
