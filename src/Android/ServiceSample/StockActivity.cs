using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ServiceSample.Services;

namespace ServiceSample
{
    [Activity(Label = "Stock Activity")]
    public class StockActivity : Activity
    {
        private bool _isBound = false;
        private StockServiceBinder _binder;
        private StockServiceConnection _stockServiceConnection;
        private StockReceiver _stockReceiver;
        private Intent _stockServiceIntent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.StockView);

            _stockServiceIntent = new Intent(StockService.IntentFilter);
            _stockReceiver = new StockReceiver();
        }

        protected override void OnStart()
        {
            base.OnStart();

            var intentFilter = new IntentFilter(StockService.StocksUpdatedAction)
                {
                    Priority = (int) IntentFilterPriority.HighPriority
                };
            RegisterReceiver(_stockReceiver, intentFilter);

            _stockServiceConnection = new StockServiceConnection(this);
            BindService(_stockServiceIntent, _stockServiceConnection, Bind.AutoCreate);

            ScheduleStockUpdates();
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (_isBound)
            {
                UnbindService(_stockServiceConnection);

                _isBound = false;
            }

            UnregisterReceiver(_stockReceiver);
        }

        private void ScheduleStockUpdates()
        {
            if (!IsAlarmSet())
            {
                var alarm = (AlarmManager) GetSystemService(Context.AlarmService);

                var pendingServiceIntent = PendingIntent.GetService(this, 0, _stockServiceIntent, PendingIntentFlags.CancelCurrent);
                alarm.SetRepeating(AlarmType.Rtc, 0, 5000, pendingServiceIntent);
                //alarm.SetRepeating (AlarmType.Rtc, 0, AlarmManager.IntervalHalfHour, pendingServiceIntent);
            }
            else
            {
                Console.WriteLine("alarm already set");
            }
        }

        private bool IsAlarmSet()
        {
            return PendingIntent.GetBroadcast(this, 0, _stockServiceIntent, PendingIntentFlags.NoCreate) != null;
        }

        private void GetStocks()
        {
            if (_isBound)
            {
                RunOnUiThread(() =>
                    {
                        DetailTextView.Text = string.Format("Stocks updated: {0}", DateTime.Now);

                        var stocks = _binder.GetStockService().GetStocks();

                        if (stocks != null)
                        {
                            StocksListView.Adapter = new ArrayAdapter<Stock>(this, Resource.Layout.StockItemView, stocks);

                        }
                        else
                        {
                            Log.Debug("StockService", "stocks is null");
                        }
                    });
            }
        }

        private class StockReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                ((StockActivity) context).GetStocks();

                InvokeAbortBroadcast();
            }
        }

        private class StockServiceConnection : Java.Lang.Object, IServiceConnection
        {
            private StockActivity _activity;

            public StockServiceConnection(StockActivity activity)
            {
                this._activity = activity;
            }

            public void OnServiceConnected(ComponentName name, IBinder service)
            {
                var stockServiceBinder = service as StockServiceBinder;
                if (stockServiceBinder != null)
                {
                    var binder = (StockServiceBinder) service;
                    _activity._binder = binder;
                    _activity._isBound = true;
                }
            }

            public void OnServiceDisconnected(ComponentName name)
            {
                _activity._isBound = false;
            }
        }

        #region Controls implementation

        private TextView _detailTextView;

        public TextView DetailTextView
        {
            get { return _detailTextView ?? (_detailTextView = FindViewById<TextView>(Resource.Id.DetailTextView)); }
        }

        private ListView _listView;

        public ListView StocksListView
        {
            get { return _listView ?? (_listView = FindViewById<ListView>(Resource.Id.StocksListView)); }
        }

        #endregion
    }
}