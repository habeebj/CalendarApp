using System;

namespace CalendarApp.Utilities
{
    public static class DateTimeExtension
    {

        /// <summary>
        /// Convert timezone DateTime to UTC DateTime
        /// </summary>
        /// <param name="timezoneId">Timezone ID eg: Europe/Warsaw</param>
        /// <returns>DateTime in UTC</returns>
        [Obsolete]
        public static DateTimeOffset ConvertTimeZoneIdToUTC(this DateTimeOffset d, string timezoneId)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var offset = timezoneInfo.GetUtcOffset(DateTime.UtcNow);
            var sign = offset.Hours < 0 ? "" : "+";
            var hours = $"{sign}{offset.Hours}";
            var munites = offset.Minutes < 0 ? offset.Minutes * -1 : offset.Minutes;
            var newFormat = $"{d.ToString("yyyy-MM-ddTHH:mm:ss")}{hours}:{munites}";
            return DateTimeOffset.Parse(newFormat).ToUniversalTime();
        }

        /// <summary>
        /// Convert timezone DateTime to UTC DateTime
        /// </summary>
        /// <param name="timezone">Timezone eg: +02:00</param>
        /// <returns>DateTime in UTC</returns>
        public static DateTimeOffset ConvertTimezoneToUTC(this DateTimeOffset d, string timezone)
        {
            var newFormat = $"{d.ToString("yyyy-MM-ddTHH:mm:ss")}{timezone}";
            d = DateTimeOffset.Parse(newFormat);
            return d.UtcDateTime;
        }

        /// <summary>
        /// Convert UTC to Timezone DateTime
        /// </summary>
        /// <param name="timezone">Timezone eg: -7:00</param>
        /// <returns>DateTime</returns>
        public static DateTimeOffset ConvertUtcToTimezoneDateTime(this DateTimeOffset d, string timezone)
        {
            return d.ToOffset(TimeSpan.Parse(timezone.Replace('+', ' ')));
        }
    }
}