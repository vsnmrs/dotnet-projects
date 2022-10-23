using System.Configuration;
using System.Collections.Specialized;

namespace coding_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? databasePath = ConfigurationManager.AppSettings.Get("databasePath");
            string? databaseConnectionString = ConfigurationManager.AppSettings.Get("connectionString");

            Console.WriteLine(databasePath + " " + databaseConnectionString);
        }
    }
}