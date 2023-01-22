using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards
{
    internal class SQLController
    {
        private SqlConnection? _connection;
        private SqlCommand? _command;
        private readonly string? _connectionString;

        public SQLController()
        {
            _connectionString = ConfigurationManager.AppSettings.Get("connectionString");
        }

        public bool ConnectToDatabase()
        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public bool CreateTable()
        {
            try
            {
                _command = new SqlCommand("CREATE TABLE TestTable(Id INT NOT NULL, Name VARCHAR(100), Date DATE)", _connection);
                _command.ExecuteNonQuery();
                Console.WriteLine("Table created successfully!");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
