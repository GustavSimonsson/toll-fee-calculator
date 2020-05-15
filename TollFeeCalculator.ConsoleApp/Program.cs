using Holiday;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System;

namespace TollFeeCalculator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====TollFeeCalculator====");
            
            var vehicleType = GetVehicleTypeInput();
            var date = GetDateInput();
            var times = GetTimesInput();

            var dateTimes = times.Select(time => date + time).ToList();

            var tollCalculator = CreateTollCalculator();
            Console.WriteLine($"The result of the calculation is the following fee: {tollCalculator.CalculateTollFee(vehicleType, dateTimes)} SEK.");
        }

        private static TollCalculator CreateTollCalculator() {
            var feeFreeVehicleChecker = new FeeFreeVehicleChecker();
            var weekendChecker = new WeekendChecker();
            var holidayChecker = new NagerHolidayChecker();
            var tollFeeGetter = new TollFeeGetter();
            var feeTimePartitioner = new FeeTimePartitioner();
            var dailyFeeCalculator = new DailyFeeCalculator(tollFeeGetter, feeTimePartitioner);

            return new TollCalculator(
                feeFreeVehicleChecker,
                weekendChecker,
                holidayChecker,
                dailyFeeCalculator);
        }

        private static VehicleType GetVehicleTypeInput() {
            Console.WriteLine($"Enter vehicle type ({GetVehicleTypesFormatted()}):");
            VehicleType vehicleType;
            while(!Enum.TryParse<VehicleType>(Console.ReadLine(), out vehicleType));
            Console.WriteLine($"The following vehicle type has been entered: '{vehicleType}'");
            return vehicleType;
        }

        private static string GetVehicleTypesFormatted() =>
            String.Join(", ",Enum.GetNames(typeof(VehicleType)));

        private static DateTime GetDateInput() {
            var dateFormat = "yyyy-MM-dd";
            Console.WriteLine($"Enter date ({dateFormat}):");
            DateTime date;
            while(!DateTime.TryParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date));
            Console.WriteLine($"The following date has been entered: '{date.ToString(dateFormat)}'");
            return date;
        }

        private static List<TimeSpan> GetTimesInput() {
            var timeFormat = "hh\\:mm";
            var readableTimeFormat = timeFormat.Replace("\\", "");
            Console.WriteLine($"Enter time(s) ({readableTimeFormat}) (minimum of one time required), enter empty input when done:");
            var times = new List<TimeSpan>();
            while (true)
            {
                var input = Console.ReadLine();
                if(TimeSpan.TryParseExact(input, timeFormat, CultureInfo.InvariantCulture, out TimeSpan time))
                {
                    times.Add(time);
                }
                
                if (!times.Any())
                {
                    Console.WriteLine("At least one correct input is required.");
                    continue;
                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
            }
            Console.WriteLine($"The following time(s) has been entered:\n{String.Join(",\n", times.Select(t => $"'{t.ToString(timeFormat)}'"))}");
            return times;
        }
    }
}
