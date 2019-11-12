using System;
using System.Linq;
using System.Collections.Generic;
using System.Json;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;


namespace ServiceSample.Services
{
    [Service]
    [IntentFilter(new [] { IntentFilter })]
    public class StockService : IntentService
    {
        private IBinder _binder;
        private IList<Stock> _stocks;

        public const string StocksUpdatedAction = "StocksUpdated";
        public const string IntentFilter = "Samples.StockService";

        protected override void OnHandleIntent(Intent intent)
        {
            var stockSymbols = new List<string>
                {
                    "AMZN",
                    "FB",
                    "GOOG",
                    "AAPL",
                    "MSFT",
                    "IBM"
                };

            _stocks = UpdateStocks(stockSymbols);

            var stocksIntent = new Intent(StocksUpdatedAction);

            SendOrderedBroadcast(stocksIntent, null);
        }

        public override IBinder OnBind(Intent intent)
        {
            _binder = new StockServiceBinder(this);
            return _binder;
        }

        public IList<Stock> GetStocks()
        {
            return _stocks;
        }

        #region Fetch stocks

        private IList<Stock> UpdateStocks(List<string> symbols)
        {
            List<Stock> results = null;

            string[] array = symbols.ToArray();
            string symbolsString = String.Join("%22%2C%22", array);

            string uri = String.Format("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22{0}%22)%0A%09%09&diagnostics=false&format=json&env=http%3A%2F%2Fdatatables.org%2Falltables.env", symbolsString);

            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));

            try
            {
                using (HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse())
                {
                    var response = httpRes.GetResponseStream();
                    var json = (JsonObject)JsonObject.Load(response);

                    results = (from result in (JsonArray)json["query"]["results"]["quote"]
                               let jResult = result as JsonObject
                               select new Stock { Symbol = jResult["Symbol"], LastPrice = (float)jResult["LastTradePriceOnly"] }).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Debug("StockService", "error connecting to web service: " + ex.Message);
            }

            return results;
        } 

        #endregion

    }

    public class StockServiceBinder : Binder
    {
        private readonly StockService _service;

        public StockServiceBinder(StockService service)
        {
            this._service = service;
        }

        public StockService GetStockService()
        {
            return _service;
        }
    }

    public class Stock
    {
        public Stock()
        {
        }

        public string Symbol { get; set; }

        public float LastPrice { get; set; }

        public override string ToString()
        {
            return string.Format("Stock: Symbol={0}, LastPrice={1}", Symbol, LastPrice);
        }
    }
}