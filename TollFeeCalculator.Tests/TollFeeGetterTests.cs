using System;
using Xunit;

namespace TollFeeCalculator.Tests
{
    public class TollFeeGetterTests
    {
        [Theory]
        [InlineData("00:00", 0)]
        [InlineData("05:59", 0)]
        [InlineData("06:00", 9)]
        [InlineData("06:29", 9)]
        [InlineData("06:30", 16)]
        [InlineData("06:59", 16)]
        [InlineData("07:00", 22)]
        [InlineData("07:59", 22)]
        [InlineData("08:00", 16)]
        [InlineData("08:29", 16)]
        [InlineData("08:30", 9)]
        [InlineData("14:59", 9)]
        [InlineData("15:00", 16)]
        [InlineData("15:29", 16)]
        [InlineData("15:30", 22)]
        [InlineData("17:00", 16)]
        [InlineData("17:59", 16)]
        [InlineData("18:00", 9)]
        [InlineData("18:29", 9)]
        [InlineData("18:30", 0)]
        [InlineData("23:59", 0)]
        [InlineData("15:00:00.001", 16)]
        [InlineData("15:29:00.001", 16)]
        public void GetTollFee_GivenTimeOfToll_ShouldReturnCorrectTollFee(string timeOfTollString, int expected)
        {
            var timeOfToll = TimeSpan.Parse(timeOfTollString);
            var sut = new TollFeeGetter();

            var actual = sut.GetTollFee(timeOfToll);
            
            Assert.Equal(expected, actual);
        }
    }
}