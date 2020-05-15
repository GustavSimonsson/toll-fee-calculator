using System.Collections.Generic;
using System;

namespace TollFeeCalculator
{
    public interface IDailyFeeCalculator
    {
        public int CalculateDailyFee(IEnumerable<TimeSpan> tollTimes);
    }
}