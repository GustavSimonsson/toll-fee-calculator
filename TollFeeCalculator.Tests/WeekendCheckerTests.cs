using System;
using Xunit;

namespace TollFeeCalculator.Tests
{
    public class WeekendCheckerTests
    {
        [Theory]
        [InlineData("2015-01-05")]
        [InlineData("2015-01-06")]
        [InlineData("2015-01-07")]
        [InlineData("2015-01-08")]
        [InlineData("2015-01-09")]
        public void IsWeekend_Weekday_ShouldReturnFalse(string dateString)
        {
            var date = DateTime.Parse(dateString);
            var sut = new WeekendChecker();

            var actual = sut.IsWeekend(date);
            
            Assert.False(actual);
        }
        
        [Theory]
        [InlineData("2015-01-10")]
        [InlineData("2015-01-11")]
        public void IsWeekend_WeekendDay_ShouldReturnTrue(string dateString)
        {
            var date = DateTime.Parse(dateString);
            var sut = new WeekendChecker();

            var actual = sut.IsWeekend(date);
            
            Assert.True(actual);
        }
    }
}