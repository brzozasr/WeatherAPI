namespace WeatherAPI.Extensions
{
    public static class Helper
    {
        public static string FirstLetterToUpper(this string str)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str[1..];
            }

            return str.ToUpper();
        }
    }
}