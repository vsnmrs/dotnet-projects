using System.Globalization;

namespace coding_tracker
{
    public static class Helper
    {
        public static bool ConvertStringToDateTime(string dateString, out DateTime dTime, out string errorMessage)
        {
            errorMessage = string.Empty;
            CultureInfo culture = CultureInfo.InvariantCulture;

            bool result = DateTime.TryParseExact(dateString, "dd.MM.yyyy HH:mm", culture, DateTimeStyles.None, out dTime);

            if (result)
                return true;
            else
            {
                errorMessage = "Incorrect date or time, try again! dd.MM.year HH:mm";
                return false;
            }
        }

        //validates the end date based on the first one (ex: end date is not before start date)
        public static bool IsEndDateValid(DateTime startDate, DateTime endDate, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (endDate <= startDate)
            {
                errorMessage = "End date should not be set before the start date!";
                return false;
            }

            TimeSpan duration = endDate - startDate;
            if (duration.TotalMinutes > 1440)
            {
                errorMessage = "No way you code more than a day staight!";
                return false;
            }

            return true;
        }
    }
}
