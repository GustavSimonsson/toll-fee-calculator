using System;

namespace TollFeeCalculator
{
    public interface IHolidayChecker
    {
        public bool IsHoliday(DateTime dateTime);
    }
}