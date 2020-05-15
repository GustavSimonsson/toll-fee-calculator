using Nager.Date;
using System;
using TollFeeCalculator;

namespace Holiday
{
    public class NagerHolidayChecker : IHolidayChecker
    {
        public bool IsHoliday(DateTime dateTime) =>
            DateSystem.IsPublicHoliday(dateTime, CountryCode.SE);
        }
}