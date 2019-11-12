using System;
using System.IO;
using Mono.Data.Sqlite;


namespace DataAccessSamples.Data
{
    public static class AdoExample
    {
        private static SqliteConnection _connection;
        private const string DatabaseName = "AdoDemo.db3";
        private static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);

        public static string DoSomeDataAccess()
        {
            var output = "";

            bool exists = File.Exists(DatabasePath);

            if (!exists)
            {
                output += "Creating database";

                // Need to create the database and seed it with some data.
                SqliteConnection.CreateFile(DatabasePath);

                _connection = new SqliteConnection("Data Source=" + DatabasePath);

                _connection.Open();

                var commands = new[]
                    {
                        "CREATE TABLE [Items] (_id ntext, Symbol ntext);",
                        "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('1', 'AAPL')",
                        "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('2', 'GOOG')",
                        "INSERT INTO [Items] ([_id], [Symbol]) VALUES ('3', 'MSFT')"
                    };

                foreach (var command in commands)
                {
                    using (var c = _connection.CreateCommand())
                    {
                        c.CommandText = command;
                        var i = c.ExecuteNonQuery();
                        output += "\n\tExecuted " + command + " (rows:" + i + ")";
                    }
                }
            }
            else
            {
                output += "Database already exists";
                _connection = new SqliteConnection("Data Source=" + DatabasePath);
                _connection.Open();
            }

            // query the database to prove data was inserted!
            using (var contents = _connection.CreateCommand())
            {
                contents.CommandText = "SELECT [_id], [Symbol] from [Items]";
                var r = contents.ExecuteReader();
                output += "\n=== Reading data;";
                while (r.Read())
                {
                    output += string.Format("\n\tKey={0}; Value={1}", r["_id"], r["Symbol"]);
                }
            }
            _connection.Close();

            return output;
        }

        public static string MoreComplexQuery()
        {
            var output = "";
            output += "\n=== Complex query example: ";

            _connection = new SqliteConnection("Data Source=" + DatabasePath);
            _connection.Open();
            using (var contents = _connection.CreateCommand())
            {
                contents.CommandText = "SELECT * FROM [Items] WHERE Symbol = 'MSFT'";
                var r = contents.ExecuteReader();
                output += "\nReading data";
                while (r.Read())
                {
                    output += String.Format("\n\tKey={0}; Value={1}", r["_id"], r["Symbol"]);
                }
            }
            _connection.Close();

            return output;
        }

        public static string ScalarQuery()
        {
            var output = "";
            output += "\n=== Scalar query example: ";

            _connection = new SqliteConnection("Data Source=" + DatabasePath);
            _connection.Open();
            using (var contents = _connection.CreateCommand())
            {
                contents.CommandText = "SELECT COUNT(*) FROM [Items] WHERE Symbol <> 'MSFT'";
                var i = contents.ExecuteScalar();
                output += "\nExecuting a scalar query";
                output += "\n\tResult=" + i;
            }
            _connection.Close();

            return output;
        }
    }
}