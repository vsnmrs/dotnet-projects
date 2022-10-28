using System.Globalization;

namespace coding_tracker
{
    public class UserInput
    {
        public static void GetUserInput()
        {
            bool closeApplication = false;

            while (!closeApplication)
            {
                Console.WriteLine("0. Close Program");
                Console.WriteLine("1. Add a session record");
                Console.WriteLine("2. List all records");

                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "0":
                        Console.WriteLine("Exiting program!");
                        closeApplication = true;
                        break;
                    case "1":
                        string date;
                        Console.WriteLine("Enter start time using the following format: dd/MM/yyyy/HH:mm");
                        date = Console.ReadLine();
                        DateTime startDate;
                        while (!ConvertStringToDateTime(date, out startDate))
                        {
                            Console.WriteLine("Incorrect date or time, try again! dd/MM/year/HH:mm");
                            date = Console.ReadLine();
                        }

                        Console.WriteLine("Enter end time using the following format: dd/MM/yyyy/HH:mm");
                        date = Console.ReadLine();
                        DateTime endDate;
                        while (!ConvertStringToDateTime(date, out endDate))
                        {
                            Console.WriteLine("Incorrect date or time, try again! dd/MM/year/HH:mm");
                            date = Console.ReadLine();
                        }

                        Console.WriteLine(startDate.ToString());
                        Console.WriteLine(endDate.ToString());
                        break;
                    default:
                        Console.WriteLine("Command not recognized, try again!");
                        break;
                }
            }
        }

        private static bool ConvertStringToDateTime(string dateString, out DateTime dTime)
        {;
            CultureInfo culture = CultureInfo.InvariantCulture;

            bool result = DateTime.TryParseExact(dateString, "dd/MM/yyyy HH:mm", culture, DateTimeStyles.None, out dTime);

            if (result)
                return true;
            else
                return false;
        }
    }
}
