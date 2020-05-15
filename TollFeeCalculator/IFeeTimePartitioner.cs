using System.Collections.Generic;
using System;

namespace TollFeeCalculator
{
    public interface IFeeTimePartitioner
    {
        public IEnumerable<IEnumerable<(TimeSpan time, int tollFee)>> PartitionBy60MinutePeriod(IEnumerable<(TimeSpan time, int tollFee)> feeByTimeOfTolls);
    }
}