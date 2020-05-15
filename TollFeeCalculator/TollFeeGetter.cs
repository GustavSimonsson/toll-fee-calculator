using System.Collections.Generic;
using System.Linq;
using System;

namespace TollFeeCalculator
{
    public class TollFeeGetter : ITollFeeGetter
    {
        public int GetTollFee(TimeSpan timeOfToll)
        {
            var timeOfTollHourMinute = CreateTimeSpanWithoutSecondsAndMilliseconds(timeOfToll);

            return GetTollFees()
                .Single(tf => IsWithinInterval((tf.start, tf.end), timeOfTollHourMinute))
                .tollFee;
        }

        private TimeSpan CreateTimeSpanWithoutSecondsAndMilliseconds(TimeSpan time) =>
            new TimeSpan(time.Hours, time.Minutes, seconds: 0);

        private bool IsWithinInterval((TimeSpan start, TimeSpan end) interval, TimeSpan time) =>
            time >= interval.start && time <= interval.end;

        private IEnumerable<(TimeSpan start, TimeSpan end, int tollFee)> GetTollFees() =>
            new[]
            {
                (TimeSpan.Parse("00:00"), TimeSpan.Parse("05:59"), 0),
                (TimeSpan.Parse("06:00"), TimeSpan.Parse("06:29"), 9),
                (TimeSpan.Parse("06:30"), TimeSpan.Parse("06:59"), 16),
                (TimeSpan.Parse("07:00"), TimeSpan.Parse("07:59"), 22),
                (TimeSpan.Parse("08:00"), TimeSpan.Parse("08:29"), 16),
                (TimeSpan.Parse("08:30"), TimeSpan.Parse("14:59"), 9),
                (TimeSpan.Parse("15:00"), TimeSpan.Parse("15:29"), 16),
                (TimeSpan.Parse("15:30"), TimeSpan.Parse("16:59"), 22),
                (TimeSpan.Parse("17:00"), TimeSpan.Parse("17:59"), 16),
                (TimeSpan.Parse("18:00"), TimeSpan.Parse("18:29"), 9),
                (TimeSpan.Parse("18:30"), TimeSpan.Parse("23:59"), 0)
            };
    }
}