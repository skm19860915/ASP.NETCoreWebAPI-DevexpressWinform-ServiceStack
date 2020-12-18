using System;
using System.Collections.Generic;

namespace Xperters.Admin.UI.Common.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="System.ComponentModel.BindingList{T}"/>.
	/// </summary>
	public static class BindingListExtensions
	{
		/// <summary>
		/// Clears all existing elements from the collection, and then adds all the new ones.
		/// If RaiseListChangedEvents is true, then the event will only be raised once;
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="bindingList"></param>
		/// <param name="collection"></param>
		public static void ReplaceAll<T>(this System.ComponentModel.BindingList<T> bindingList, IEnumerable<T> collection)
		{
			if (bindingList == null)
				throw new ArgumentNullException(nameof(bindingList));
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			// Remember the current setting for RaiseListChangedEvents
			var oldRaiseEventsValue = bindingList.RaiseListChangedEvents;
			try
			{
				bindingList.RaiseListChangedEvents = false;
				bindingList.Clear();
			}
			finally
			{
				bindingList.RaiseListChangedEvents = oldRaiseEventsValue;
			}

			bindingList.AddRange(collection);
		}

		/// <summary>
		/// Adds the elements of the specified collection to the end of the <see cref="System.ComponentModel.BindingList{T}"/>,
		/// while only firing the <see cref="System.ComponentModel.BindingList{T}.ListChanged"/>-event once.
		/// </summary>
		/// <typeparam name="T">
		/// The type T of the values of the <see cref="System.ComponentModel.BindingList{T}"/>.
		/// </typeparam>
		/// <param name="bindingList">
		/// The <see cref="System.ComponentModel.BindingList{T}"/> to which the values shall be added.
		/// </param>
		/// <param name="collection">
		/// The collection whose elements should be added to the end of the <see cref="System.ComponentModel.BindingList{T}"/>.
		/// The collection itself cannot be null, but it can contain elements that are null,
		/// if type T is a reference type.
		/// </param>
		/// <exception cref="ArgumentNullException">values is null.</exception>
		public static void AddRange<T>(this System.ComponentModel.BindingList<T> bindingList, IEnumerable<T> collection)
		{
			// The given collection may not be null.
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			// Remember the current setting for RaiseListChangedEvents
			// (if it was already deactivated, we shouldn't activate it after adding!).
			var oldRaiseEventsValue = bindingList.RaiseListChangedEvents;

			// Try adding all of the elements to the binding list.
			try
			{
				bindingList.RaiseListChangedEvents = false;

				foreach (var value in collection)
					bindingList.Add(value);
			}

			// Restore the old setting for RaiseListChangedEvents (even if there was an exception),
			// and fire the ListChanged-event once (if RaiseListChangedEvents is activated).
			finally
			{
				bindingList.RaiseListChangedEvents = oldRaiseEventsValue;

				if (bindingList.RaiseListChangedEvents)
					bindingList.ResetBindings();
			}
		}
	}
}
