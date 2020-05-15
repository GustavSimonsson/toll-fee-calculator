using NSubstitute;
using System.Collections.Generic;
using System;
using Xunit;

namespace TollFeeCalculator.Tests
{
    public class TollCalculatorTests
    {
        private readonly IEnumerable<DateTime> dummyDates = new [] { DateTime.MinValue };
        
        [Fact]
        public void CalculateTollFee_FeeFreeVehicle_ShouldReturnZero()
        {
            var sut = CreateSut(isFeeFreeVehicle: true);

            var actual = sut.CalculateTollFee(default, dummyDates);
            
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CalculateTollFee_NonFeeFreeVehicle_ShouldReturnNonZero()
        {
            var sut = CreateSut(isFeeFreeVehicle: false);

            var actual = sut.CalculateTollFee(default, dummyDates);
            
            Assert.NotEqual(0, actual);
        }
        
        [Fact]
        public void CalculateTollFee_OnAHoliday_ShouldReturnZero()
        {
            var sut = CreateSut(isHoliday: true);

            var actual = sut.CalculateTollFee(default, dummyDates);
            
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CalculateTollFee_OnANonHoliday_ShouldReturnNonZero()
        {
            var sut = CreateSut(isHoliday: false);

            var actual = sut.CalculateTollFee(default, dummyDates);
            
            Assert.NotEqual(0, actual);
        }
        
        [Fact]
        public void CalculateTollFee_OnAWeekend_ShouldReturnZero()
        {
            var sut = CreateSut(isWeekend: true);

            var actual = sut.CalculateTollFee(default, dummyDates);
            
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CalculateTollFee_OnAWeekday_ShouldReturnNonZero()
        {
            var sut = CreateSut(isWeekend: false);

            var actual = sut.CalculateTollFee(default, dummyDates);
            
            Assert.NotEqual(0, actual);
        }

        private TollCalculator CreateSut(bool isFeeFreeVehicle = false, bool isWeekend = false, bool isHoliday = false, int tollFee = 1)
        {
            var feeFreeVehicleChecker = Substitute.For<IFeeFreeVehicleChecker>();
            feeFreeVehicleChecker.IsFeeFreeVehicle(default).ReturnsForAnyArgs(isFeeFreeVehicle);
            
            var weekendChecker = Substitute.For<IWeekendChecker>();
            weekendChecker.IsWeekend(default).ReturnsForAnyArgs(isWeekend);

            var holidayChecker = Substitute.For<IHolidayChecker>();
            holidayChecker.IsHoliday(default).ReturnsForAnyArgs(isHoliday);
            
            var dailyFeeCalculator = Substitute.For<IDailyFeeCalculator>();
            dailyFeeCalculator.CalculateDailyFee(default).ReturnsForAnyArgs(tollFee);

            return new TollCalculator(feeFreeVehicleChecker, weekendChecker, holidayChecker, dailyFeeCalculator);
        }
    }
}
