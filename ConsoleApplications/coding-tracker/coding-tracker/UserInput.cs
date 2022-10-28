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
                        Console.WriteLine("Enter start time using the following format: dd/MM/year");
                        string date = Console.ReadLine();
                        DateTime d;
                        while (!ConvertStringToDateTime(date, out d))
                        {
                            Console.WriteLine("Incorrect date or time, try again! dd/MM/year");
                            date = Console.ReadLine();
                        }

                        Console.WriteLine(d.ToString());
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

            bool result = DateTime.TryParseExact(dateString, "dd/MM/yyyy", culture, DateTimeStyles.None, out dTime);

            if (result)
                return true;
            else
                return false;
        }
    }
}
