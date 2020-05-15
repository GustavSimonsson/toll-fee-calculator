using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;

namespace Holiday.Tests
{
    public class NagerHolidayCheckerTests
    {
        [Theory]
        [MemberData(nameof(AllHolidays2018TestData))]
        public void IsHoliday_WhenDateIsASwedishHoliday_ShouldReturnTrue(DateTime swedishHoliday)
        {
            var sut = new NagerHolidayChecker();

            var actual = sut.IsHoliday(swedishHoliday);
            
            Assert.True(actual);
        }
        
        [Theory]
        [MemberData(nameof(AllNonHolidays2018TestData))]
        public void IsHoliday_WhenDateIsNotASwedishHoliday_ShouldReturnFalse(DateTime notASwedishHoliday)
        {
            var sut = new NagerHolidayChecker();

            var actual = sut.IsHoliday(notASwedishHoliday);
            
            Assert.False(actual);
        }

        public static IEnumerable<object[]> AllHolidays2018TestData() =>
            AllHolidays2018.Select(x => new object[] {x}).ToArray();
        
        public static IEnumerable<object[]> AllNonHolidays2018TestData() =>
            AllDays2018.Except(AllHolidays2018).Select(x => new object[] {x}).ToArray();
        
        private static IEnumerable<DateTime> AllHolidays2018 =>
            new[]
            {
                DateTime.Parse("2018-01-01"),
                DateTime.Parse("2018-01-06"),
                DateTime.Parse("2018-03-30"),
                DateTime.Parse("2018-04-01"),
                DateTime.Parse("2018-04-02"),
                DateTime.Parse("2018-05-01"),
                DateTime.Parse("2018-05-10"),
                DateTime.Parse("2018-05-20"),
                DateTime.Parse("2018-06-06"),
                DateTime.Parse("2018-06-22"),
                DateTime.Parse("2018-06-23"),
                DateTime.Parse("2018-11-03"),
                DateTime.Parse("2018-12-24"),
                DateTime.Parse("2018-12-25"),
                DateTime.Parse("2018-12-26"),
                DateTime.Parse("2018-12-31")
            };

        private static IEnumerable<DateTime> AllDays2018 =>
            Enumerable.Range(0, 365)
                .Select(daysToAdd => DateTime.Parse("2018-01-01").AddDays(daysToAdd))
                .ToArray();
    }
}
