using Domain.Enums;

namespace Domain.Extensions
{
    public static class VehicleTypesExtension
    {
        public static bool IsTollFreeVehicle(this VehicleTypes vehicleType) => vehicleType.Equals(VehicleTypes.Motorbike) ||
                                                                               vehicleType.Equals(VehicleTypes.Tractor) ||
                                                                               vehicleType.Equals(VehicleTypes.Emergency) ||
                                                                               vehicleType.Equals(VehicleTypes.Diplomat) ||
                                                                               vehicleType.Equals(VehicleTypes.Foreign) ||
                                                                               vehicleType.Equals(VehicleTypes.Military);
    }
}
