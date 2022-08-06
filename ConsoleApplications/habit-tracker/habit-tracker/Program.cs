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

            bool quitApplication = false;

            while (!quitApplication)
            {
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Add Entry");
                Console.WriteLine("2. Edit Entry");
                Console.WriteLine("3. Delete Entry");
                Console.WriteLine("4. List All Entries");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "0":
                        {
                            Console.WriteLine("Exiting application");
                            quitApplication = true;
                            break;
                        }
                    case "1":
                        {
                            Console.WriteLine("Habit Name: ");
                            string? name = Console.ReadLine();

                            Console.WriteLine("Value (must be int): ");
                            string? sValue = Console.ReadLine();
                            int value;
                            bool isParsable = Int32.TryParse(sValue, out value);

                            if (isParsable)
                                _sqliteConnection.InsertRecord(name, value);
                            else
                                Console.WriteLine("Error: Value entered is not a number");

                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Enter the name of the habit you want to edit: ");
                            string? name = Console.ReadLine();

                            Console.WriteLine("Enter the new value: ");
                            string? sValue = Console.ReadLine();
                            int value;
                            bool isParsable = Int32.TryParse(sValue, out value);

                            if (isParsable)
                                _sqliteConnection.UpdateRecord(name, value);
                            else
                                Console.WriteLine("Error: Value entered is not a number");

                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Eneter the name of the habit to be deleted: ");
                            string? name = Console.ReadLine();
                            _sqliteConnection.DeleteRecord(name);

                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Listing all records:");
                            _sqliteConnection.ReadDatabaseTable();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Command not recognized!");
                            break;
                        }
                }
            }
        }
    }
}