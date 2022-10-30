using System.Configuration;

namespace coding_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? databasePath = ConfigurationManager.AppSettings.Get("databasePath");
            string? databaseName = ConfigurationManager.AppSettings.Get("databaseName");

            if (databasePath != null && databaseName != null)
            {
                SQLiteDBConnection connection = new SQLiteDBConnection(databasePath, databaseName);

                UserInput.GetUserInput(connection);
            }
            else
            {
                Console.WriteLine("Could not load configuration file!");
            }
        }
    }
}