using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Android.App;
using Android.Views;
using Android.Widget;


namespace Android
{

    public abstract class ObservableCollectionAdapter<T> : BaseAdapter<T>
    {
        private readonly int _resource;
        private readonly ObservableCollection<T> _items;

        protected ObservableCollectionAdapter(Activity context, int resource, ObservableCollection<T> items)
            : base()
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            if (resource <= 0)
            {
                throw new ArgumentOutOfRangeException("resource");
            }

            this.Context = context;
            this._resource = resource;
            this._items = items;
            this._items.CollectionChanged += this.OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.NotifyDataSetChanged();
        }

        private void OnItemChanged(object sender, EventArgs e)
        {
            this.NotifyDataSetChanged();
        }

        public override T this[int position]
        {
            get { return this._items[position]; }
        }

        protected Activity Context { get; private set; }

        public override int Count
        {
            get { return this._items.Count; }
        }

        public override long GetItemId(int position)
        {
            return this.GetItemId(this._items[position], position);
        }

        private readonly Dictionary<View, T> _initializedViews = new Dictionary<View, T>();

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView != null)
            {
                T oldItem;
                if (this._initializedViews.TryGetValue(convertView, out oldItem))
                {
                    var oldObservable = oldItem as INotifyPropertyChanged;
                    if (oldObservable != null)
                    {
                        oldObservable.PropertyChanged -= this.OnItemChanged;
                    }
                }
            }

            View view = convertView;
            if (view == null)
            {
                view = this.Context.LayoutInflater.Inflate(_resource, null);
                this.InitializeNewView(view);
            }

            T item = this[position];
            this._initializedViews[view] = item;
            this.PrepareView(item, view);

            var observable = item as INotifyPropertyChanged;
            if (observable != null)
            {
                observable.PropertyChanged += this.OnItemChanged;
            }

            return view;
        }

        protected virtual void InitializeNewView(View view)
        {
        }

        protected abstract void PrepareView(T item, View view);

        protected abstract long GetItemId(T item, int position);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._items.CollectionChanged -= this.OnCollectionChanged;
            }

            base.Dispose(disposing);
        }
    }
}