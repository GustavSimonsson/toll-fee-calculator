namespace TollFeeCalculator
{
    public class FeeFreeVehicleChecker : IFeeFreeVehicleChecker
    {
        public bool IsFeeFreeVehicle(VehicleType vehicleType) =>
            vehicleType switch
            {
                VehicleType.Motorcycle => true,
                VehicleType.Moped => true,
                VehicleType.Tractor => true,
                _ => false
            };
    }
}