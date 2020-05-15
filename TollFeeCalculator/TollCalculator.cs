using System.Collections.Generic;
using System.Linq;
using System;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        private readonly IFeeFreeVehicleChecker feeFreeVehicleChecker;
        private readonly IWeekendChecker weekendChecker;
        private readonly IHolidayChecker holidayChecker;
        private readonly IDailyFeeCalculator dailyFeeCalculator;
        private const int noTollFee = 0;

        public TollCalculator(IFeeFreeVehicleChecker feeFreeVehicleChecker, IWeekendChecker weekendChecker, IHolidayChecker holidayChecker, IDailyFeeCalculator dailyFeeCalculator)
        {
            this.feeFreeVehicleChecker = feeFreeVehicleChecker;
            this.weekendChecker = weekendChecker;
            this.holidayChecker = holidayChecker;
            this.dailyFeeCalculator = dailyFeeCalculator;
        }

        public int CalculateTollFee(VehicleType vehicleType, IEnumerable<DateTime> dates) =>
            feeFreeVehicleChecker.IsFeeFreeVehicle(vehicleType)
            || weekendChecker.IsWeekend(dates.First())
            || holidayChecker.IsHoliday(dates.First())
                ? noTollFee
                : dailyFeeCalculator.CalculateDailyFee(dates.Select(x => x.TimeOfDay));
    }
}
