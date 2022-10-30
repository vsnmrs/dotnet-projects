namespace coding_tracker
{
    public class UserInput
    {
        public static void GetUserInput(SQLiteDBConnection db)
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
                        {
                            string date;
                            string errorMessage;
                            Console.WriteLine("Enter start time using the following format: dd.MM.yyyy HH:mm");
                            date = Console.ReadLine();
                            DateTime startDate;

                            while (!Helper.ConvertStringToDateTime(date, out startDate, out errorMessage))
                            {
                                Console.WriteLine("Incorrect date or time, try again! dd.MM.year HH:mm");
                                date = Console.ReadLine();
                            }

                            Console.WriteLine("Enter end time using the following format: dd.MM.yyyy HH:mm");
                            date = Console.ReadLine();
                            DateTime endDate;

                            while (!Helper.ConvertStringToDateTime(date, out endDate, out errorMessage) || !Helper.IsEndDateValid(startDate, endDate, out errorMessage))
                            {
                                Console.WriteLine(errorMessage);
                                date = Console.ReadLine();
                            }

                            CodingSessionRecord record = new CodingSessionRecord(startDate, endDate);

                            Console.WriteLine("Total session duration was {0} minutes", record.SessionDuration);

                            db.InsertRecord(record);
                        }
                        break;
                    case "2":
                        {
                            List<CodingSessionRecord> records = db.ReadDBTable();

                            TableVisualisation tableVisualisation = new(records);
                            tableVisualisation.DisplayTable();

                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Command not recognized, try again!");
                        break;
                }
            }
        }
    }
}
