using System.Collections.Generic;
using System.Linq;
using System;

namespace TollFeeCalculator
{
    public class DailyFeeCalculator : IDailyFeeCalculator
    {
        private readonly ITollFeeGetter tollFeeGetter;
        private readonly IFeeTimePartitioner feeTimePartitioner;
        private const int maxDailyFee = 60;

        public DailyFeeCalculator(ITollFeeGetter tollFeeGetter, IFeeTimePartitioner feeTimePartitioner)
        {
            this.tollFeeGetter = tollFeeGetter;
            this.feeTimePartitioner = feeTimePartitioner;
        }

        public int CalculateDailyFee(IEnumerable<TimeSpan> tollTimes)
        {
            var feeByTollTimes = tollTimes.Select(x => (x, tollFeeGetter.GetTollFee(x)));
            var feesWithin60MinutePeriod = feeTimePartitioner.PartitionBy60MinutePeriod(feeByTollTimes);
            var highestFeePer60MinutePeriod = feesWithin60MinutePeriod.Select(x => x.Max(y => y.tollFee));
            var totalFee = highestFeePer60MinutePeriod.Sum();
            var limitedFee = totalFee > maxDailyFee ? maxDailyFee : totalFee;

            return limitedFee;
        }
    }
}