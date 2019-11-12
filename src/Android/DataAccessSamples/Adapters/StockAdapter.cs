using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DataAccessSamples.Data;


namespace DataAccessSamples.Adapters
{
    public class StockAdapter : ObservableCollectionAdapter<Stock>
    {
        public StockAdapter(Activity context, int resource, ObservableCollection<Stock> items) : base(context, resource, items)
        {
        }

        protected override void PrepareView(Stock item, View view)
        {
            view.FindViewById<TextView>(Resource.Id.StockName).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.StockSymbol).Text = item.Symbol;
        }

        protected override long GetItemId(Stock item, int position)
        {
            return 0;
        }
    }
}