using System.Data.SQLite;

namespace habit_tracker
{
    public class MySQLiteConnection
    {
        private SQLiteConnection _connection;

        public MySQLiteConnection(string dbName)
        {
            _connection = new SQLiteConnection($"Data Source={dbName}");

            try
            {
                _connection.Open();
                Console.WriteLine($"Connection to the {dbName} database established!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetSQLiteVersion()
        {
            var command = new SQLiteCommand("SELECT SQLITE_VERSION()", _connection);
            string? version = command.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version {version}");
        }

        public void CreateHabbitsTable()
        {
            var command = new SQLiteCommand(_connection);
            command.CommandText = @"CREATE TABLE IF NOT EXISTS habits(id INTEGER PRIMARY KEY, name TEXT, value INT)";
            command.ExecuteNonQuery();
        }
    }
}
