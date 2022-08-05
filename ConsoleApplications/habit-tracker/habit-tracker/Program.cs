using System.Data.SQLite;

namespace habit_tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            MySQLiteConnection _sqliteConnection = new MySQLiteConnection("habits.db");
            _sqliteConnection.GetSQLiteVersion();
            _sqliteConnection.CreateHabbitsTable("habits");
            _sqliteConnection.InsertData("drink", 40);
        }
    }
}