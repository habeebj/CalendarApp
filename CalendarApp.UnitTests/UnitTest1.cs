using System.Linq;
using System.Globalization;
using System;
using CalendarApp.Domain.Entities;
using NUnit.Framework;
using CalendarApp.Utilities;

namespace CalendarApp.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // var date = DateTime.Parse("2021-04-30T07:48:00");
            // Console.WriteLine(date.FromTimezoneToUniversalTime("+02:00"));
            // Console.WriteLine(date.FromTimezoneToUniversalTime("-07:00"));
            // date = DateTime.Parse("2021-04-30T15:45:00");
            // Console.WriteLine(date.FromTimezoneToUniversalTime("+05:45"));
            
            
            // var now = DateTimeOffset.UtcNow;

            // // var timezone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
            // // var timezone = TimeZoneInfo.FindSystemTimeZoneById("Africa/Lagos");
            // var timezone = TimeZoneInfo.FindSystemTimeZoneById("America/Los_Angeles");
            // var offset = timezone.GetUtcOffset(DateTime.UtcNow);
            // var sign = offset.Hours < 0 ? "" : "+";
            // var newFormat = $"{date.ToString("yyyy-MM-ddThh:m:ss")}{sign}{offset.Hours}:{offset.Minutes}";

            // // Console.WriteLine($"{offset}");
            // Console.WriteLine(DateTime.Parse(newFormat).ToUniversalTime());
            // // Console.WriteLine(newFormat);
            // // Console.WriteLine($"{date}{sign}{offset}");


            // // var tzWarsaw = 2;

            // // var lagosDate = new DateTime(2021, 04, 30, 6, 28, 0);
            // // var warsawTime = new DateTimeOffset(lagosDate.ToUniversalTime());

            // // Console.WriteLine(lagosDate);
            // // Console.WriteLine(warsawTime);
            // // Console.WriteLine(lagosDate.ToUniversalTime());

            // // var _dateTime = new DateTime(2021, 04, 30, 7, 20, 0, DateTimeKind.Unspecified);

            // // var dateTimeUnSpec = DateTime.SpecifyKind(_dateTime, DateTimeKind.Unspecified);
            // // var date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(_dateTime, "Europe/Warsaw");
            // // System.Console.WriteLine(date.ToString("TZD"));

            // var warsawTime = DateTime.Parse("2021-04-30T07:48:00-07:00").ToUniversalTime();
            // var lagosDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(warsawTime, "Africa/Lagos");

            // var t = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
            // var timespan = t.GetUtcOffset(warsawTime);
            // Console.WriteLine(timespan);


            var timezones = TimeZoneInfo.GetSystemTimeZones().Select(t => new { t.Id, t.DisplayName, t.DaylightName, t.StandardName });
            Console.WriteLine(string.Join(",\n", timezones));


            // Console.WriteLine(lagosDate.ToLongTimeString());
            // Console.WriteLine(warsawTime.ToLongTimeString());

            Assert.Pass();
        }
    }
}