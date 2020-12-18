using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid.Rows;
using Xperters.Admin.ServiceModel.Extensions;

namespace Xperters.Admin.UI.Common.GridDefinition
{
	public sealed class ColumnDefinition<T> : GridElementDefinition<T>, IColumnDefinition
	{
		public string PropertyPath { get; private set; }
		private List<Action<EditorRow>> _editorRowModifiers = new List<Action<EditorRow>>();
		public IEnumerable<Action<EditorRow>> EditorRowModifiers { get { return _editorRowModifiers; } }
		public Func<RepositoryItem> RepositoryItemFactory { get; private set; } = () => null;
		public bool IsReadOnly { get; private set; }

		private List<string> _contextExclusions = new List<string>();
		public IEnumerable<string> ContextExclusions { get { return _contextExclusions.AsReadOnly(); } }

		public ColumnDefinition<T> WithField<TReturn>(
			Expression<Func<T, TReturn>> propertyExpression)
		{
			if (propertyExpression == null)
				throw new ArgumentNullException(nameof(propertyExpression));

			string propertyPath = propertyExpression.GetPropertyPath();

			WithField(propertyPath);
			return this;
		}

		public ColumnDefinition<T> WithField(
			string propertyPath)
		{
			if (string.IsNullOrWhiteSpace(propertyPath))
				throw new ArgumentException(nameof(propertyPath), "value cannot be null or whitespace");

			PropertyPath = propertyPath;
			return this;
		}

		public ColumnDefinition<T> WithTitle(string title)
		{
			Title = title;
			return this;
		}

		public ColumnDefinition<T> WithTooltip(string tooltip)
		{
			Tooltip = tooltip;
			return this;
		}

		public ColumnDefinition<T> MakeInvisible()
		{
			IsVisible = false;
			return this;
		}

		public ColumnDefinition<T> AsReadOnly()
		{
			IsReadOnly = true;
			return this;
		}

		public ColumnDefinition<T> ExcludeFromContexts(params string[] contexts)
		{
			_contextExclusions.AddRange(contexts ?? new string[] { });
			return this;
		}

		public ColumnDefinition<T> WithColumn()
		{
			var columnDefinition = new ColumnDefinition<T>();
			_gridElementDefinitions.Add(columnDefinition);

			return columnDefinition;
		}

		public ColumnDefinition<T> WithRepositoryItem(Func<RepositoryItem> repositoryItemFactory)
		{
			RepositoryItemFactory = repositoryItemFactory;
			return this;
		}

		public ColumnDefinition<T> WithCustomEditRowProperties(Action<EditorRow> editorRowModifier)
		{
			if (editorRowModifier != null)
				_editorRowModifiers.Add(editorRowModifier);
			return this;
		}

		public ColumnDefinition<T> WithIgnoresMask()
		{
			IgnoresMask = true;
			return this;
		}
	}
}
