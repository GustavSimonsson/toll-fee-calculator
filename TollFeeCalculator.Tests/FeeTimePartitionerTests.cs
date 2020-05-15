using System;
using Xunit;

namespace TollFeeCalculator.Tests
{
    public class FeeTimePartitionerTests
    {
        [Fact]
        public void PartitionBy60MinutePeriod_FeeByTimeOfTollsOnlyContainingOneTime_ShouldReturnListWithPeriodWithOneItem()
        {
            var feeByTimeOfTolls = new[]
            {
                (TimeSpan.Parse("10:00"), 1000)
            };
            var sut = new FeeTimePartitioner();

            var actual = sut.PartitionBy60MinutePeriod(feeByTimeOfTolls);

            var expectedPeriod = new[]
            {
                (TimeSpan.Parse("10:00"), 1000)
            };
            var expected = new[]
            {
                expectedPeriod
            };
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void PartitionBy60MinutePeriod_FeeByTimeOfTollsContainingTwoTimesWithinSame60MinutePeriod_ShouldReturnListWithOnePeriodWithTwoItems()
        {
            var feeByTimeOfTolls = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("10:15"), 1015)
            };
            var sut = new FeeTimePartitioner();

            var actual = sut.PartitionBy60MinutePeriod(feeByTimeOfTolls);

            var expectedPeriod = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("10:15"), 1015)
            };
            var expected = new[]
            {
                expectedPeriod
            };
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void PartitionBy60MinutePeriod_FeeByTimeOfTollsContainingTwoTimesWithinDifferent60MinutePeriod_ShouldReturnListWithTwoPeriodsWithOneItem()
        {
            var feeByTimeOfTolls = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("11:15"), 1115)
            };
            var sut = new FeeTimePartitioner();

            var actual = sut.PartitionBy60MinutePeriod(feeByTimeOfTolls);

            var expectedPeriod1 = new[]
            {
                (TimeSpan.Parse("10:00"), 1000)
            };
            var expectedPeriod2 = new[]
            {
                (TimeSpan.Parse("11:15"), 1115)
            };
            var expected = new[]
            {
                expectedPeriod1,
                expectedPeriod2
            };
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void PartitionBy60MinutePeriod_FeeByTimeOfTollsContainingThreeTimesWithinDifferent60MinutePeriodInNonChronologicalOrder_ShouldReturnListWithTwoPeriods()
        {
            var feeByTimeOfTolls = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("11:15"), 1115),
                (TimeSpan.Parse("10:15"), 1015)
                
            };
            var sut = new FeeTimePartitioner();

            var actual = sut.PartitionBy60MinutePeriod(feeByTimeOfTolls);

            var expectedPeriod1 = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("10:15"), 1015),
            };
            var expectedPeriod2 = new[]
            {
                (TimeSpan.Parse("11:15"), 1115)
            };
            var expected = new[]
            {
                expectedPeriod1,
                expectedPeriod2
            };
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void PartitionBy60MinutePeriod_FeeByTimeOfTollsContainingBoundaryValueOneMillisecondBeforeNewHour_ShouldReturnListWithOnePeriod()
        {
            var feeByTimeOfTolls = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("10:59:59.999"), 105959999),

            };
            var sut = new FeeTimePartitioner();

            var actual = sut.PartitionBy60MinutePeriod(feeByTimeOfTolls);

            var expectedPeriod1 = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("10:59:59.999"), 105959999)
            };
            var expected = new[]
            {
                expectedPeriod1
            };
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void PartitionBy60MinutePeriod_FeeByTimeOfTollsContainingBoundaryValueOneHour_ShouldReturnListWithTwoPeriods()
        {
            var feeByTimeOfTolls = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("11:00"), 1100)

            };
            var sut = new FeeTimePartitioner();

            var actual = sut.PartitionBy60MinutePeriod(feeByTimeOfTolls);

            var expectedPeriod1 = new[]
            {
                (TimeSpan.Parse("10:00"), 1000)
            };
            var expectedPeriod2 = new[]
            {
                (TimeSpan.Parse("11:00"), 1100)
            };
            var expected = new[]
            {
                expectedPeriod1,
                expectedPeriod2
            };
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void PartitionBy60MinutePeriod_AdvancedExample_ShouldReturnExpected()
        {
            var feeByTimeOfTolls = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("10:15"), 1015),
                (TimeSpan.Parse("10:45"), 1045),
                (TimeSpan.Parse("11:05"), 1105),
                (TimeSpan.Parse("11:45"), 1145),
                (TimeSpan.Parse("12:04"), 1204),
                (TimeSpan.Parse("12:30"), 1230),
                (TimeSpan.Parse("12:35"), 1235),
                (TimeSpan.Parse("12:45"), 1245),
                (TimeSpan.Parse("13:03"), 1303),
                (TimeSpan.Parse("13:04"), 1304),
                (TimeSpan.Parse("13:30:00"), 1330)
            };
            
            var sut = new FeeTimePartitioner();
            
            var actual = sut.PartitionBy60MinutePeriod(feeByTimeOfTolls);
            
            var expectedPeriod1 = new[]
            {
                (TimeSpan.Parse("10:00"), 1000),
                (TimeSpan.Parse("10:15"), 1015),
                (TimeSpan.Parse("10:45"), 1045)
            };
            var expectedPeriod2 = new[]
            {
                (TimeSpan.Parse("11:05"), 1105),
                (TimeSpan.Parse("11:45"), 1145),
                (TimeSpan.Parse("12:04"), 1204)
            };
            var expectedPeriod3 = new[]
            {
                (TimeSpan.Parse("12:30"), 1230),
                (TimeSpan.Parse("12:35"), 1235),
                (TimeSpan.Parse("12:45"), 1245),
                (TimeSpan.Parse("13:03"), 1303),
                (TimeSpan.Parse("13:04"), 1304),
            };
            var expectedPeriod4 = new[]
            {
                (TimeSpan.Parse("13:30:00"), 1330)
            };
            var expected = new[]
            {
                expectedPeriod1,
                expectedPeriod2,
                expectedPeriod3,
                expectedPeriod4
            };
            Assert.Equal(expected, actual);
        }
    }
}