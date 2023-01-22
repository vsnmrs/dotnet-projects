using System.Configuration;

namespace Flashcards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SQLController controller = new SQLController();
            controller.ConnectToDatabase();
            controller.CreateTable();
            controller.CloseConnection();
        }
    }
}