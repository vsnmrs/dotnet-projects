using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Flashcards
{
    internal class SQLController
    {
        public SQLController()
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection("Server=(LocalDb)\\FlashCardsDB;Database=Flashcards;Trusted_Connection=True;");

                SqlCommand sqlCommand = new SqlCommand("CREATE TABLE TestTable(Id INT NOT NULL, Name VARCHAR(100), Date DATE)", connection);

                connection.Open();

                sqlCommand.ExecuteNonQuery();

                Console.WriteLine("Table created successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
