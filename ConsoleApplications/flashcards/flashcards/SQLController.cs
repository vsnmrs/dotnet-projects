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

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public bool CreateTables()
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

                Console.WriteLine("Tables created successfully or already exist!");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void TestInsertion(string name)
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
