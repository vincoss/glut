using System;
using System.Collections.Generic;


namespace DataAccessSamples.Data
{
    public class StockRepository
    {
        private readonly StockDatabase _db;
        protected  static StockRepository Instance;

        static StockRepository()
        {
            Instance = new StockRepository();
        }

        protected StockRepository()
        {
            _db = new StockDatabase(StockDatabase.DatabaseFilePath);
        }

        public static Stock GetStock(int id)
        {
            return Instance._db.GetStock(id);
        }

        public static IEnumerable<Stock> GetStocks()
        {
            return Instance._db.GetStocks();
        }

        public static int SaveStock(Stock item)
        {
            return Instance._db.SaveStock(item);
        }

        public static int DeleteStock(Stock item)
        {
            return Instance._db.DeleteStock(item);
        }
    }
}