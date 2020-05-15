using System;

namespace TollFeeCalculator
{
    public interface IWeekendChecker
    {
        public bool IsWeekend(DateTime date);
    }
}