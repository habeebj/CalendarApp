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
        public static DateTime ConvertTimeZoneIdToUTC(this DateTime d, string timezoneId)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var offset = timezoneInfo.GetUtcOffset(DateTime.UtcNow);
            var sign = offset.Hours < 0 ? "" : "+";
            var hours = $"{sign}{offset.Hours}";
            var munites = offset.Minutes < 0 ? offset.Minutes * -1 : offset.Minutes;
            var newFormat = $"{d.ToString("yyyy-MM-ddTHH:mm:ss")}{hours}:{munites}";
            return DateTime.Parse(newFormat).ToUniversalTime();
        }

        /// <summary>
        /// Convert timezone DateTime to UTC DateTime
        /// </summary>
        /// <param name="timezone">Timezone eg: +02:00</param>
        /// <returns>DateTime in UTC</returns>
        public static DateTime ConvertTimezoneToUTC(this DateTime d, string timezone)
        {
            // expected timezone value = +hh:m
            // split timezone by ':'
            var timeOffset = timezone.Split(':');
            if (timeOffset.Length != 2)
                throw new ArgumentException(nameof(timezone));

            var hour = int.Parse(timeOffset[0]);
            var munites = int.Parse(timeOffset[1]);

            return d.Add(new TimeSpan(-1 * hour, -1 * munites, 0));
        }

        /// <summary>
        /// Convert UTC to Timezone DateTime
        /// </summary>
        /// <param name="timezone">Timezone eg: -7:00</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertUtcToTimezoneDateTime(this DateTime d, string timezone)
        {
            var timeOffset = timezone.Split(':');
            if (timeOffset.Length != 2)
                throw new ArgumentException(nameof(timezone));

            var hour = int.Parse(timeOffset[0]);
            var munites = int.Parse(timeOffset[1]);
            return d.Subtract(new TimeSpan(-1 * hour, -1 * munites, 0));
        }
    }
}