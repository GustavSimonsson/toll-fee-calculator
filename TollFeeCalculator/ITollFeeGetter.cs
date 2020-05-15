using System;

namespace TollFeeCalculator
{
    public interface ITollFeeGetter
    {
        public int GetTollFee(TimeSpan timeOfToll);
    }
}