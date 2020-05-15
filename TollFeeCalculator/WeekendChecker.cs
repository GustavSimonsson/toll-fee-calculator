using System;

namespace TollFeeCalculator
{
    public class WeekendChecker : IWeekendChecker
    {
        public bool IsWeekend(DateTime date) =>
            date.DayOfWeek switch
            {
                DayOfWeek.Saturday => true,
                DayOfWeek.Sunday => true,
                _ => false
            };
    }
}