using Microsoft.SqlServer.Server;
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

                _command = _connection.CreateCommand();

                //get the SQL version
                Console.WriteLine("Getting the SQL Server version...");
                _command.CommandText = "SELECT @@version";
                string? version = _command.ExecuteScalar().ToString();
                Console.WriteLine(version);
                Console.WriteLine();

                CreateTables();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        private bool CreateTables()
        {
            try
            {
                _command.CommandText =
                    @"IF OBJECT_ID(N'dbo.Stacks', N'U') IS NULL CREATE TABLE dbo.Stacks(Id INT IDENTITY(1000,1) PRIMARY KEY,
                                                                                        Name VARCHAR(100) NOT NULL UNIQUE)";

                _command.ExecuteNonQuery();

                _command.CommandText =
                    @"IF OBJECT_ID(N'dbo.Flashcards', N'U') IS NULL CREATE TABLE dbo.Flashcards(Id INT IDENTITY(1000,1) PRIMARY KEY,
                                                                                               Front VARCHAR(100),
                                                                                               Back VARCHAR(100),
                                                                                               Stack INT FOREIGN KEY REFERENCES dbo.Stacks(Id))";

                _command.ExecuteNonQuery();

                Console.WriteLine("Tables created successfully or already exist! \n");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<string> GetStacksList()
        {
            List<string> stacksList = new List<string>();

            _command.CommandText = "SELECT * FROM dbo.Stacks";

            SqlDataReader dataReader = _command.ExecuteReader();

            while(dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string name = dataReader.GetString(1);

                stacksList.Add(name);
            }

            dataReader.Close();

            return stacksList;
        }

        public void AddNewStackElement(string name)
        {
            _command.CommandText = $"INSERT INTO dbo.Stacks(Name) VALUES ('{name}')";
            _command.ExecuteNonQuery();
        }

        public void TestAddingFlashcards(string front, string back, int stackKey)
        {
            _command.CommandText = $"INSERT INTO dbo.Flashcards(Front, Back, Stack) VALUES('{front}', '{back}', {stackKey})";
            _command.ExecuteNonQuery();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}
