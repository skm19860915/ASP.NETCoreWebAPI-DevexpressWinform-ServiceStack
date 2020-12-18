using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Xperters.Core.Collections
{
    /// <summary>
    /// This class provides a method to allow multiple entries to be added
    /// to an ObservableCollection without the CollectionChanged event being fired.
    /// <para>
    /// This class does not take support any thread synchronization of
    /// adding items using multiple threads, that level of thread synchronization
    /// is _left to the user. This class simply marshalls the CollectionChanged
    /// call to the correct Dispatcher thread.
    /// </para>
    /// This class was taken and subsequently modified from
    /// <c>http://peteohanlon.wordpress.com/2008/10/22/bulk-loading-in-observablecollection/</c>
    /// </summary>
    /// <typeparam name="T">Type this collection holds</typeparam>
    public class AddRangeObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suppressNotification;

        public AddRangeObservableCollection()
        {
        }

        public AddRangeObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        /// <summary>
        /// Adds a range of items to the Collection, without firing the
        /// CollectionChanged event
        /// </summary>
        /// <param name="list">The items to add</param>
        public void AddRange(IEnumerable<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            _suppressNotification = true;

            foreach (T item in list)
            {
                Add(item);
            }

            _suppressNotification = false;
            OnCollectionChanged(new
                NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Only raise the OnCollectionChanged event if there is currently no suppressed notification.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
            {
                base.OnCollectionChanged(e);
            }
        }
    }
}
