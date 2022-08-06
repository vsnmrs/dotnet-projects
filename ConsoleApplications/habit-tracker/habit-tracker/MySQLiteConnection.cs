﻿using System.Data.SQLite;

namespace habit_tracker
{
    public class MySQLiteConnection
    {
        private SQLiteConnection _connection;
        private SQLiteCommand _command;
        private string _tableName;

        public MySQLiteConnection(string dbName)
        {
            _connection = new SQLiteConnection($"Data Source={dbName}");

            try
            {
                _connection.Open();
                _command = new SQLiteCommand(_connection);

                Console.WriteLine($"Connection to the {dbName} database established!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetSQLiteVersion()
        {
            _command.CommandText = @"SELECT SQLITE_VERSION()";
            string? version = _command.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version {version}");
        }

        public void CreateHabbitsTable(string tableName)
        {
            _tableName = tableName;
            _command.CommandText = $"CREATE TABLE IF NOT EXISTS {_tableName}(id INTEGER PRIMARY KEY, name TEXT, value INT)";
            _command.ExecuteNonQuery();
        }

        public void InsertRecord(string habitName, int habitValue)
        {
            _command.CommandText = $"INSERT INTO {_tableName}(name, value) VALUES('{habitName}',{habitValue})";
            _command.ExecuteNonQuery();
        }

        public void ReadDatabaseTable()
        {
            _command.CommandText = $"SELECT * FROM {_tableName}";
            SQLiteDataReader dataReader = _command.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine($"{dataReader.GetInt32(0)} {dataReader.GetString(1)} {dataReader.GetInt32(2)}");
            }

            Console.WriteLine();

            dataReader.Close();
        }

        public void UpdateRecord(string habit, int newValue)
        {
            _command.CommandText = $"UPDATE {_tableName} SET value = {newValue} WHERE name = '{habit}'";
            _command.ExecuteNonQuery();
        }

        public void DeleteRecord(string habit)
        {
            _command.CommandText = $"DELETE FROM {_tableName} WHERE name = '{habit}'";
            _command.ExecuteNonQuery();
        }
    }
}
