using System.Collections.Generic;
using System.Linq;
using System;

namespace TollFeeCalculator
{
    public class FeeTimePartitioner : IFeeTimePartitioner
    {
        public IEnumerable<IEnumerable<(TimeSpan time, int tollFee)>> PartitionBy60MinutePeriod(IEnumerable<(TimeSpan time, int tollFee)> feeByTimeOfTolls)
        {
            var feeByTimeOfTollsOrdered = feeByTimeOfTolls.OrderBy(x => x.time).ToList();
            
            var startInterval = feeByTimeOfTollsOrdered.First().time;
            var endInterval = startInterval.Add(TimeSpan.FromMinutes(60));
            var intervals = new List<List<(TimeSpan time, int tollFee)>>();
            var currentInterval = new List<(TimeSpan time, int tollFee)>();

            foreach (var feeByTimeOfToll in feeByTimeOfTollsOrdered) {
                if (feeByTimeOfToll.time >= startInterval && feeByTimeOfToll.time < endInterval) {
                    currentInterval.Add(feeByTimeOfToll);
                    continue;
                }

                intervals.Add(currentInterval);
                startInterval = feeByTimeOfToll.time;
                endInterval = startInterval.Add(TimeSpan.FromMinutes(60));
                currentInterval = new List<(TimeSpan time, int tollFee)>();
                currentInterval.Add(feeByTimeOfToll);
            }
            intervals.Add(currentInterval);

            return intervals;
        }
    }
}