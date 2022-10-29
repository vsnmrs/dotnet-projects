using ConsoleTableExt;

namespace coding_tracker
{
    public class TableVisualisation
    {
        private List<List<object>> _tableData;
        public TableVisualisation(List<CodingSessionRecord> records)
        {
            _tableData = new List<List<object>>();

            foreach (var record in records)
            {
                _tableData.Add(new List<object> { record.ID, record.SessionStart, record.SessionEnd, record.SessionDuration });
            }
        }

        public void DisplayTable()
        {
            ConsoleTableBuilder
                .From(_tableData)
                .WithColumn("ID", "Session Start", "Session End", "Duration (minutes)")
                .ExportAndWriteLine();
        }
    }
}
