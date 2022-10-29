using System.Globalization;

namespace coding_tracker
{
    public static class Helper
    {
        public static bool ConvertStringToDateTime(string dateString, out DateTime dTime)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;

            bool result = DateTime.TryParseExact(dateString, "dd.MM.yyyy HH:mm", culture, DateTimeStyles.None, out dTime);

            if (result)
                return true;
            else
                return false;
        }
    }
}
