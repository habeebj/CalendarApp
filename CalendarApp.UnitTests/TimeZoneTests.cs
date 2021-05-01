using System;
using CalendarApp.Utilities;
using NUnit.Framework;

namespace CalendarApp.UnitTests
{
    public class TimeZoneTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertToUtcDateTime_WithNegativeTimezone_ShouldBeSuccessful()
        {
            // Los Angeles timezone = GMT-07:00
            var date = DateTime.Parse("2021-04-30T21:00:00");
            var utcDateTime = date.ConvertTimezoneToUTC("-07:00");
            Assert.AreEqual(DateTime.Parse("2021-05-01T04:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertToUtcDateTime_WithPositiveTimezone_ShouldBeSuccessful()
        {
            // Warsaw timezone = GMT+02:00
            var date = DateTime.Parse("2021-05-01T06:00:00");
            var utcDateTime = date.ConvertTimezoneToUTC("+02:00");
            Assert.AreEqual(DateTime.Parse("2021-05-01T04:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertToUtcDateTime_NegativeTimezoneWithMunites_ShouldBeSuccessful()
        {
            // St. John's timezone = GMT-03:30
            var date = DateTime.Parse("2021-05-01T06:30:00");
            var utcDateTime = date.ConvertTimezoneToUTC("-03:30");
            Assert.AreEqual(DateTime.Parse("2021-05-01T09:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertToUtcDateTime_PositiveTimezoneWithMunites_ShouldBeSuccessful()
        {
            // Kathmandu timezone = GMT+05:45
            var date = DateTime.Parse("2021-05-01T09:45:00");
            var utcDateTime = date.ConvertTimezoneToUTC("+05:45");
            Assert.AreEqual(DateTime.Parse("2021-05-01T04:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertToUtcDateFromTomeZoneId_WithNegativeTimezone_ShouldBeSuccessful()
        {
            // Los Angeles timezone = America/Los_Angeles | GMT-07:00
            var date = DateTime.Parse("2021-04-30T21:00:00");
            var utcDateTime = date.ConvertTimeZoneIdToUTC("America/Los_Angeles");
            Assert.AreEqual(DateTime.Parse("2021-05-01T04:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertToUtcDateFromTomeZoneId_WithPositiveTimezone_ShouldBeSuccessful()
        {
            // Warsaw timezone = Europe/Warsaw | GMT+02:00
            var date = DateTime.Parse("2021-05-01T06:00:00");
            var utcDateTime = date.ConvertTimeZoneIdToUTC("Europe/Warsaw");
            Assert.AreEqual(DateTime.Parse("2021-05-01T04:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertToUtcDateFromTomeZoneId_NegativeTimezoneWithMunites_ShouldBeSuccessful()
        {
            // St. John's timezone = America/St_Johns | GMT-03:30
            var date = DateTime.Parse("2021-05-01T06:30:00");
            var utcDateTime = date.ConvertTimeZoneIdToUTC("America/St_Johns");
            Assert.AreEqual(DateTime.Parse("2021-05-01T09:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertToUtcDateFromTomeZoneId_PositiveTimezoneWithMunites_ShouldBeSuccessful()
        {
            // Kathmandu timezone ID = Asia/Kathmandu | GMT+05:45
            var date = DateTime.Parse("2021-05-01T09:45:00");
            var utcDateTime = date.ConvertTimeZoneIdToUTC("Asia/Kathmandu");
            Assert.AreEqual(DateTime.Parse("2021-05-01T04:00:00"), utcDateTime);
        }

        [Test]
        public void ConvertUtcToTimeZone_PositiveTimezone_ShouldBeSuccessful()
        {
            var dateTime = DateTime.Parse("2021-05-01T10:59:00");
            var dateTimeInTimezone = dateTime.ConvertUtcToTimezoneDateTime("+02:00");
            Assert.AreEqual(DateTime.Parse("2021-05-01T12:59:00"), dateTimeInTimezone);
        }

        [Test]
        public void ConvertUtcToTimeZone_NegativeTimezone_ShouldBeSuccessful()
        {
            var dateTime = DateTime.Parse("2021-05-01T06:59:00");
            var dateTimeInTimezone = dateTime.ConvertUtcToTimezoneDateTime("-07:00");
            Assert.AreEqual(DateTime.Parse("2021-04-30T23:59:00"), dateTimeInTimezone);
        }

        [Test]
        public void ConvertUtcToTimeZone_PositiveTimezoneWithMunites_ShouldBeSuccessful()
        {
            var dateTime = DateTime.Parse("2021-05-01T06:00:00");
            var dateTimeInTimezone = dateTime.ConvertUtcToTimezoneDateTime("+05:45");
            Assert.AreEqual(DateTime.Parse("2021-05-01T11:45:00"), dateTimeInTimezone);
        }
        [Test]
        public void sample()
        {
            // Warsaw timezone = GMT+02:00
            var date = DateTime.Parse("2021-05-01T18:53:00");
            var utcDateTime = date.ConvertTimezoneToUTC("+02:00");
            Assert.AreEqual(DateTime.Parse("2021-05-01T16:53:00"), utcDateTime);
        }
    }
}