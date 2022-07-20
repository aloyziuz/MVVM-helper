using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Data;

namespace MVVMHelper
{
    public class ObservableCollectionWithBatchAdd<T> : ObservableCollection<T>
    {
        internal DeferredEventsCollection _deferredEvents;

        public ObservableCollectionWithBatchAdd(): base()
        {

        }

        public ObservableCollectionWithBatchAdd(IEnumerable<T> collection) : base(collection)
        {

        }

        public ObservableCollectionWithBatchAdd(List<T> list) : base(list)
        {

        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                Add(item);

            var e = new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, new List<T>(items));
            this.OnCollectionChanged(e);
        }



        /// <summary>
        /// Raise CollectionChanged event to any listeners.
        /// Properties/methods modifying this ObservableCollection will raise
        /// a collection changed event through this virtual method.
        /// </summary>
        /// <remarks>
        /// When overriding this method, either call its base implementation
        /// or call <see cref="BlockReentrancy"/> to guard against reentrant collection changes.
        /// </remarks>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var _deferredEvents = (ICollection<NotifyCollectionChangedEventArgs>)typeof(ObservableCollectionWithBatchAdd<T>).GetField("_deferredEvents", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
            if (_deferredEvents != null)
            {
                _deferredEvents.Add(e);
                return;
            }

            foreach (var handler in GetHandlers())
                if (IsRange(e) && handler.Target is CollectionView cv)
                    cv.Refresh();
                else
                    handler(this, e);
        }

        private bool IsRange(NotifyCollectionChangedEventArgs e)
        {
            return e.NewItems?.Count > 1 || e.OldItems?.Count > 1;
        }

        internal IEnumerable<NotifyCollectionChangedEventHandler> GetHandlers()
        {
            var info = typeof(ObservableCollection<T>).GetField(nameof(CollectionChanged), BindingFlags.Instance | BindingFlags.NonPublic);
            var @event = (MulticastDelegate)info.GetValue(this);
            return @event?.GetInvocationList()
              .Cast<NotifyCollectionChangedEventHandler>()
              .Distinct()
              ?? Enumerable.Empty<NotifyCollectionChangedEventHandler>();
        }
    }

    internal class DeferredEventsCollection : List<NotifyCollectionChangedEventArgs>, IDisposable
    {
        private readonly ObservableCollectionWithBatchAdd<object> _collection;

        public DeferredEventsCollection(ObservableCollectionWithBatchAdd<object> collection)
        {
            Debug.Assert(collection != null);
            Debug.Assert(collection._deferredEvents == null);
            _collection = collection;
            _collection._deferredEvents = this;
        }

        public void Dispose()
        {
            _collection._deferredEvents = null;

            var handlers = _collection
              .GetHandlers()
              .ToLookup(h => h.Target is CollectionView);

            foreach (var handler in handlers[false])
                foreach (var e in this)
                    handler(_collection, e);

            foreach (var cv in handlers[true]
              .Select(h => h.Target)
              .Cast<CollectionView>()
              .Distinct())
                cv.Refresh();
        }
    }
}
