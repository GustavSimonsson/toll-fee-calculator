namespace TollFeeCalculator
{
    public interface IFeeFreeVehicleChecker
    {
        public bool IsFeeFreeVehicle(VehicleType vehicleType);
    }
}