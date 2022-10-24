namespace coding_tracker
{
    public class CodingSessionRecord
    {
        private int _id;
        private string _sessionStart, _sessionEnd;
        private int _sessionDuration; //in minutes, is auto-calculated, not entered manually

        private int ID { get { return _id; } }
        private int SessionDuration { get { return _sessionDuration; } }

        private string SessionStart { get { return _sessionStart; } }
        private string SessionEnd { get { return _sessionEnd; } }

        public CodingSessionRecord(string sessionStart, string sessionEnd)
        {
            _sessionStart = sessionStart;
            _sessionEnd = sessionEnd;
            _sessionDuration = CalculateDuration();
        }

        private int CalculateDuration()
        {
            return 0;
        }
    }
}
