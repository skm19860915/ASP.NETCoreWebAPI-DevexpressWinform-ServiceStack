using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xperters.Admin.ServiceModel.Extensions;
using Xperters.Admin.ServiceModel.Validations;

namespace Xperters.Admin.UI.Common.GridDefinition
{
	public abstract class GridDefinitionBuilder<T> : IGridDefinitionBuilder, IPropertyTitleProvider
	{
		public ColumnGroupDefinition<T> TopColumGroupDefinition { get; }
		private ColumnDefinition<T> NullColumnDefinition { get; } = new ColumnDefinition<T>();
		public string PropertyPathPrefix { get; }
		private ConcurrentDictionary<string, string> PropertyPathToTitleMap { get; set; } = new ConcurrentDictionary<string, string>();

		public GridDefinitionBuilder(Expression<Func<T, object>> propertyPathPrefixExpression, params string[] contexts)
		{
			if (propertyPathPrefixExpression == null)
				throw new ArgumentNullException(nameof(propertyPathPrefixExpression));

			PropertyPathPrefix = propertyPathPrefixExpression.GetPropertyPath();

			var columnGroupDefinition = new ColumnGroupDefinition<T>(contexts ?? new string[] { });
			TopColumGroupDefinition = columnGroupDefinition;
		}

		private IColumnGroupDefinition GetAnchorGroup(IColumnGroupDefinition columnGroupDefinition, string context)
		{
			if (columnGroupDefinition.Contexts.Contains(context))
				return columnGroupDefinition;
			else
			{
				foreach (var childColumnGroupDefinition in columnGroupDefinition.GridElementDefinitions.OfType<IColumnGroupDefinition>())
				{
					var anchor = GetAnchorGroup(childColumnGroupDefinition, context);
					if (anchor != null)
					{
						return anchor;
					}
				}
			}

			return null;
		}

		public IColumnGroupDefinition GetAnchorGroup(string context)
		{
			var anchor = GetAnchorGroup(TopColumGroupDefinition, context);
			if (anchor == null)
				throw new UiException($"No anchor element defined for '{context}'");
			return anchor;
		}

		public IEnumerable<IColumnDefinition> GetAllColumnDefinitions()
		{
			List<ColumnDefinition<T>> columnDefinitions = new List<ColumnDefinition<T>>();

			return GetColumnDefinitions(new[] { TopColumGroupDefinition })
				.ToList();
		}

		private IEnumerable<ColumnDefinition<T>> GetColumnDefinitions(IEnumerable<IGridElementDefinition> gridElementDefinitions)
		{
			foreach (var element in gridElementDefinitions)
			{
				if (element is ColumnDefinition<T> columnDefinition)
					yield return columnDefinition;

				foreach (var definition in GetColumnDefinitions(element.GridElementDefinitions))
				{
					yield return definition;
				}
			}
		}

		public IColumnDefinition GetColumnDefinition(string propertyPath)
		{
			if (propertyPath == null)
				throw new ArgumentNullException(nameof(propertyPath));

			if (string.IsNullOrWhiteSpace(propertyPath))
				return NullColumnDefinition;

			return GetAllColumnDefinitions().FirstOrDefault(o => o.PropertyPath.EndsWith(propertyPath)) ?? NullColumnDefinition;
		}

		public IColumnDefinition GetColumnDefinition<TReturn>(
			Expression<Func<T, TReturn>> propertyExpression)
		{
			if (propertyExpression == null)
				throw new ArgumentNullException(nameof(propertyExpression));

			string propertyPath = propertyExpression.GetPropertyPath();

			return GetColumnDefinition(propertyPath);
		}

		public bool HasColumnDefinition(string propertyPath)
		{
			if (propertyPath == null)
				throw new ArgumentNullException(nameof(propertyPath));

			if (string.IsNullOrWhiteSpace(propertyPath))
				return false;

			//TODO: I added a null check here as we were getting an exception on startup.
			return GetAllColumnDefinitions().FirstOrDefault(o => o.PropertyPath != null && o.PropertyPath.EndsWith(propertyPath)) != null;
		}

		public bool HasColumnDefinition<TReturn>(
			Expression<Func<T, TReturn>> propertyExpression)
		{
			if (propertyExpression == null)
				throw new ArgumentNullException(nameof(propertyExpression));

			string propertyPath = propertyExpression.GetPropertyPath();

			return HasColumnDefinition(propertyPath);
		}

		public void Compile()
		{
			var gridColumnDefinitions = GetAllColumnDefinitions();
			PropertyPathToTitleMap = new ConcurrentDictionary<string, string>(gridColumnDefinitions
				.GroupBy(o => o.PropertyPath)
				.Select(o => o.First())
				.Where(o => !string.IsNullOrWhiteSpace(o.PropertyPath))
				.ToDictionary(o => o.PropertyPath, o => o.Title));
		}

		public string GetTitle(string propertyPath)
		{
			if (PropertyPathToTitleMap.TryGetValue(propertyPath, out string title))
				return title;
			else
				return null;
		}
	}
}
