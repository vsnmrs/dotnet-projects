using System.Data.SQLite;

namespace coding_tracker
{
    public class SQLiteDBConnection
    {
        private SQLiteConnection _connection;
        private SQLiteCommand _command;
        private string _tableName;

        private List<CodingSessionRecord> _recordsList;

        public SQLiteDBConnection(string dbPath, string dbName)
        {
            _connection = new SQLiteConnection($"Data Source={dbPath}{dbName}");

            try
            {
                _connection.Open();
                Console.WriteLine($"Connection to {dbName} established!");

                _command = new SQLiteCommand(_connection);

                GetSQLiteVersion();
                CreateDBTable("CodingSessions");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetSQLiteVersion()
        {
            _command.CommandText = @"SELECT SQLITE_VERSION()";
            string? version = _command.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version {version} running.");
        }

        private void CreateDBTable(string tableName)
        {
            _tableName = tableName;
            _command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}(id INTEGER PRIMARY KEY, startTime TEXT, endTime TEXT, duration INTEGER)";
            _command.ExecuteNonQuery();
        }

        public void InsertRecord(CodingSessionRecord session)
        {
            _command.CommandText = $"INSERT INTO {_tableName}(startTime, endTime, duration) VALUES('{session.SessionStart}','{session.SessionEnd}','{session.SessionDuration}')";
            _command.ExecuteNonQuery();
        }

        public void UpdateRecord()
        {

        }

        public void DeleteRecord(int id)
        {
            _command.CommandText = $"DELETE FROM {_tableName} WHERE id = '{id}'";
            _command.ExecuteNonQuery();
        }

        public List<CodingSessionRecord> ReadDBTable()
        {
            _command.CommandText = $"SELECT * FROM {_tableName}";
            SQLiteDataReader dataReader = _command.ExecuteReader();

            _recordsList = new List<CodingSessionRecord>();

            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);

                string startDateString = dataReader.GetString(1);
                string endDateString = dataReader.GetString(2);

                //remove the last 3 characters from the string which represents the seconds
                startDateString = startDateString.Substring(0, startDateString.Length - 3);
                endDateString = endDateString.Substring(0, endDateString.Length - 3);

                DateTime startDate, endDate;
                string errorMessage;
                Helper.ConvertStringToDateTime(startDateString, out startDate, out errorMessage);
                Helper.ConvertStringToDateTime(endDateString, out endDate, out errorMessage);

                if (errorMessage.Length > 0)
                    Console.WriteLine(errorMessage);

                CodingSessionRecord record = new(startDate, endDate)
                {
                    ID = id
                };

                _recordsList.Add(record);
            }

            dataReader.Close();

            return _recordsList;
        }
    }
}
