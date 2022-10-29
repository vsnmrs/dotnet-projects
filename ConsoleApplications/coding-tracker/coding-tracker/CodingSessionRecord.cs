namespace coding_tracker
{
    public class CodingSessionRecord
    {
        private int _id;
        private DateTime _sessionStart, _sessionEnd;
        private double _sessionDuration; //in minutes, is auto-calculated, not entered manually

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public double SessionDuration { get { return _sessionDuration; } }
        public DateTime SessionStart { get { return _sessionStart; } }
        public DateTime SessionEnd { get { return _sessionEnd; } }

        public CodingSessionRecord(DateTime sessionStart, DateTime sessionEnd)
        {
            _sessionStart = sessionStart;
            _sessionEnd = sessionEnd;
            _sessionDuration = CalculateDuration();
        }

        private double CalculateDuration()
        {
            TimeSpan duration = _sessionEnd - _sessionStart;
            return duration.TotalMinutes;
        }
    }
}
