using System.Data.SQLite;

namespace habit_tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection connection = CreateConnection();
            GetSQLiteVersion(connection);
            CreateHabbitsTable(connection);
        }

        private static SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection("Data Source=habits.db");

            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return connection;
        }

        private static void GetSQLiteVersion(SQLiteConnection connection)
        {
            var command = new SQLiteCommand("SELECT SQLITE_VERSION()", connection);
            string? version = command.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version {version}");
        }

        private static void CreateHabbitsTable(SQLiteConnection connection)
        {
            var command = new SQLiteCommand(connection);
            command.CommandText = @"CREATE TABLE IF NOT EXISTS habits(id INTEGER PRIMARY KEY, name TEXT, value INT)";
            command.ExecuteNonQuery();
        }
    }
}