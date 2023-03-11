using System.Configuration;

namespace Flashcards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SQLController controller = new SQLController();
            controller.ConnectToDatabase();
            controller.CreateTables();

            //controller.TestInsertion("Romanian");
            //controller.TestInsertion("French");
            //controller.TestInsertion("Dutch");
            //controller.TestInsertion("Italian");

            //controller.TestAddingFlashcards("chair", "scaun", 1000);

            UserInput.GetUserInput();

            controller.CloseConnection();
        }
    }
}