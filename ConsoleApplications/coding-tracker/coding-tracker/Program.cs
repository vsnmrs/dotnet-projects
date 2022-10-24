using System.Configuration;
using System.Collections.Specialized;

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
            }
            else
            {
                Console.WriteLine("Could not load configuration file!");
            }
        }
    }
}