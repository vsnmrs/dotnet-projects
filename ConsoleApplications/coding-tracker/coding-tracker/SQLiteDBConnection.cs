using System.Data.SQLite;

namespace coding_tracker
{
    public class SQLiteDBConnection
    {
        private SQLiteConnection _connection;
        private SQLiteCommand _command;

        public SQLiteDBConnection(string dbPath, string dbName)
        {
            _connection = new SQLiteConnection($"Data Source={dbPath}{dbName}");

            try
            {
                _connection.Open();
                Console.WriteLine($"Connection to {dbName} established!");

                _command = new SQLiteCommand(_connection);

                GetSQLiteVersion();
                CreateDBTable("CodingSessions");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetSQLiteVersion()
        {
            _command.CommandText = @"SELECT SQLITE_VERSION()";
            string? version = _command.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version {version} running.");
        }

        private void CreateDBTable(string tableName)
        {
            _command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}(id INTEGER PRIMARY KEY, startTime TEXT, endTime TEXT, duration INTEGER)";
            _command.ExecuteNonQuery();
        }
    }
}
