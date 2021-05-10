using System;

namespace Core.Utilities.TimeCalculator
{
    public static class RelativeTimeCalculator
    {
        public static string Calculate(DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds + " saniye önce";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " dakika önce";

            if (delta < 24 * HOUR)
                return ts.Hours + " saat önce";

            if (delta < 48 * HOUR)
                return "dün";

            if (delta < 30 * DAY)
                return ts.Days + " gün önce";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months + " ay önce";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years + " yıl önce";
            }
        }
    }
}