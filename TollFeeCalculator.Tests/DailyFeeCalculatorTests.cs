using NSubstitute;
using System;
using Xunit;

namespace TollFeeCalculator.Tests
{
    public class DailyFeeCalculatorTests
    {
        [Theory]
        [InlineData("08:00", 10, 10)]
        [InlineData("10:00", 15, 15)]
        [InlineData("12:00", 20, 20)]
        public void CalculateDailyFee_GivenOneTollTime_ShouldReturnCorrespondingFee(string timeString, int fee, int expected)
        {
            var (sut, tollFeeGetter, feeTimePartitioner) = CreateSut();
            var time = TimeSpan.Parse(timeString);
            var tollTimes = new[] { time };
            tollFeeGetter.GetTollFee(default).ReturnsForAnyArgs(fee);
            var feeByTollTime = (time, fee);
            var inputTollTimes = new[]
            {
                feeByTollTime
            };
            var partitionedTollTimes = new[]
            {
                new[] {feeByTollTime},
            };
            feeTimePartitioner.PartitionBy60MinutePeriod(inputTollTimes).ReturnsForAnyArgs(partitionedTollTimes);

            var actual = sut.CalculateDailyFee(tollTimes);
            
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("08:00", 10, "10:00", 15, 25)]
        [InlineData("10:00", 15, "12:00", 20, 35)]
        public void CalculateDailyFee_GivenMultipleTollTimes_ShouldReturnCorrespondingFee(string timeString1, int fee1, string timeString2, int fee2, int expected)
        {
            var (sut, tollFeeGetter, feeTimePartitioner) = CreateSut();
            var time1 = TimeSpan.Parse(timeString1);
            var time2 = TimeSpan.Parse(timeString2);
            var tollTimes = new[] { time1, time2 };
            tollFeeGetter.GetTollFee(time1).Returns(fee1);
            tollFeeGetter.GetTollFee(time2).Returns(fee2);
            var feeByTollTime1 = (time1, fee1);
            var feeByTollTime2 = (time2, fee2);
            var inputTollTimes = new[]
            {
                feeByTollTime1, feeByTollTime2
            };
            var partitionedTollTimes = new[]
            {
                new[] {feeByTollTime1},
                new[] {feeByTollTime2}
            };
            feeTimePartitioner.PartitionBy60MinutePeriod(inputTollTimes).ReturnsForAnyArgs(partitionedTollTimes);

            var actual = sut.CalculateDailyFee(tollTimes);
            
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("08:00", 10, "08:30", 20, 20)]
        public void CalculateDailyFee_GivenMultipleTollTimesWithin60Minutes_ShouldReturnTheHighestFee(string timeString1, int fee1, string timeString2, int fee2, int expected)
        {
            var (sut, tollFeeGetter, feeTimePartitioner) = CreateSut();
            var time1 = TimeSpan.Parse(timeString1);
            var time2 = TimeSpan.Parse(timeString2);
            var tollTimes = new[] { time1, time2 };
            tollFeeGetter.GetTollFee(time1).Returns(fee1);
            tollFeeGetter.GetTollFee(time2).Returns(fee2);
            var feeByTollTime1 = (time1, fee1);
            var feeByTollTime2 = (time2, fee2);
            var inputTollTimes = new[]
            {
                feeByTollTime1, feeByTollTime2
            };
            var partitionedTollTimes = new[]
            {
                new[] {feeByTollTime1, feeByTollTime2}
            };
            feeTimePartitioner.PartitionBy60MinutePeriod(inputTollTimes).ReturnsForAnyArgs(partitionedTollTimes);

            var actual = sut.CalculateDailyFee(tollTimes);
            
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("08:00", 60, 60)]
        [InlineData("10:00", 61, 60)]
        [InlineData("12:00", 1000, 60)]
        public void CalculateDailyFee_GivenFeeOverMaxFee_ShouldReturnMaxFee(string timeString, int fee, int expected)
        {
            var (sut, tollFeeGetter, feeTimePartitioner) = CreateSut();
            var time = TimeSpan.Parse(timeString);
            var tollTimes = new[] { time };
            tollFeeGetter.GetTollFee(default).ReturnsForAnyArgs(fee);
            var feeByTollTime = (time, fee);
            var inputTollTimes = new[]
            {
                feeByTollTime
            };
            var partitionedTollTimes = new[]
            {
                new[] {feeByTollTime},
            };
            feeTimePartitioner.PartitionBy60MinutePeriod(inputTollTimes).ReturnsForAnyArgs(partitionedTollTimes);

            var actual = sut.CalculateDailyFee(tollTimes);
            
            Assert.Equal(expected, actual);
        }

        private (DailyFeeCalculator sut, ITollFeeGetter tollFeeGetter, IFeeTimePartitioner feeTimePartitioner) CreateSut()
        {
            var tollFeeGetter = Substitute.For<ITollFeeGetter>();

            var feeTimePartitioner = Substitute.For<IFeeTimePartitioner>();

            return (new DailyFeeCalculator(tollFeeGetter, feeTimePartitioner), tollFeeGetter, feeTimePartitioner);
        }
    }
}