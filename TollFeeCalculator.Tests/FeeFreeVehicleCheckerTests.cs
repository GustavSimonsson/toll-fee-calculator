using Xunit;

namespace TollFeeCalculator.Tests
{
    public class FeeFreeVehicleCheckerTests
    {
        [Theory]
        [InlineData(VehicleType.Motorcycle)]
        [InlineData(VehicleType.Moped)]
        [InlineData(VehicleType.Tractor)]
        public void CalculateTollFee_FeeFreeVehicle_ShouldReturnZero(VehicleType vehicleType)
        {
            var sut = new FeeFreeVehicleChecker();

            var actual = sut.IsFeeFreeVehicle(vehicleType);
            
            Assert.True(actual);
        }

        [Theory]
        [InlineData(VehicleType.Bus)]
        [InlineData(VehicleType.Car)]
        [InlineData(VehicleType.Truck)]
        public void CalculateTollFee_FeeEligibleVehicle_ShouldReturnNonZero(VehicleType vehicleType)
        {
            var sut = new FeeFreeVehicleChecker();

            var actual = sut.IsFeeFreeVehicle(vehicleType);
            
            Assert.False(actual);
        }
    }
}