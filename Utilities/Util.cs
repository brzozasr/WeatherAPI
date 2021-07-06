using System;

namespace WeatherAPI.Utilities
{
    public class Util
    {
        public static DateTime? UnixTimeToDateTime(long unixTime)
        {
            if (unixTime > 0)
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
            }

            return null;
        }
        
        public static DateTime? UnixTimeToDateTimeLocal(long unixTime, long timezoneOffset)
        {
            if (unixTime > 0)
            {
                var localTime = unixTime + timezoneOffset;
                return DateTimeOffset.FromUnixTimeSeconds(localTime).DateTime;
            }

            return null;
        }

        public static float? MPerSecToKmPerH(float? mPerSec)
        {
            return (float?) Math.Round(
                mPerSec ?? 0 * 3.6f, 2, MidpointRounding.ToZero) == 0
                ? null
                : (float?) Math.Round(
                    mPerSec ?? 0 * 3.6f, 2, MidpointRounding.ToZero);
        }
        
        public static float? MPerSecToKmPerH(float mPerSec)
        {
            return (float?) Math.Round(mPerSec * 3.6f, 2, MidpointRounding.ToZero);
        }

        public static float? MToKm(int meters)
        {
            return (float?) Math.Round(meters / 1000.0f, 2,
                MidpointRounding.ToZero);
        }
    }
}