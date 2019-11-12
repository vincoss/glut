using System;
using System.Data;
using System.IO;
using System.Linq;
using SQLite;


namespace DataAccessSamples.Data
{
    public static class OrmExample
    {
        private const string DatabaseName = "OrmDemo.db3";

        private static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);

        /// <returns>
        /// Output of test query
        /// </returns>
        public static string DoSomeDataAccess()
        {
            var output = "";
            output += "\nCreating database, if it doesn't already exist";

            var db = GetConnection();
            db.CreateTable<Stock>();

            if (db.Table<Stock>().Any() == false)
            {
                // only insert the data if it doesn't already exist
                var newStock = new Stock();
                newStock.Symbol = "AAPL";
                db.Insert(newStock);

                newStock = new Stock();
                newStock.Symbol = "GOOG";
                db.Insert(newStock);

                newStock = new Stock();
                newStock.Symbol = "MSFT";
                db.Insert(newStock);
            }
           

            output += "\nReading data using Orm";
            var table = db.Table<Stock>();
            foreach (var s in table)
            {
                output += "\n" + s.Id + " " + s.Symbol;
            }

            return output;
        }

        public static string MoreComplexQuery()
        {
            var output = "";
            output += "\nComplex query example: ";

            var db = GetConnection();

            var query = db.Query<Stock>("SELECT * FROM [Items] WHERE Symbol = ?", "MSFT");
            foreach (var s in query)
            {
                output += "\n" + s.Id + " " + s.Symbol;
            }

            return output;
        }

        public static string Get()
        {
            var output = "";
            output += "\nGet query example: ";

            var db = GetConnection();

            var returned = db.Get<Stock>(3);

            return output;
        }

        public static string Delete()
        {
            var output = "";
            output += "\nDelete query example: ";

            var db = GetConnection();

            var rowcount = db.Delete(new Stock() { Id = 3 });

            return output;
        }

        public static string ScalarQuery()
        {
            var output = "";
            output += "\nScalar query example: ";

            var db = GetConnection();

            var rowcount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM [Items] WHERE Symbol <> ?", "MSFT");

            output += "\nNumber of rows : " + rowcount;

            return output;
        }

        public static string GetWithLinq()
        {
            var db = GetConnection();

            var items = from s in db.Table<Stock>()
                        where s.Symbol.StartsWith("A")
                        select s;

            return items.FirstOrDefault().Symbol;
        }

        private static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(DatabasePath);
        }
    }
}

