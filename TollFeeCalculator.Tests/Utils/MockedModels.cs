using Domain.Entities;
using Domain.Enums;

namespace TollFeeCalculator.Tests.Utils
{
    public static class MockedModels
    {
        public static Car Car => new Car
        {
            Type = VehicleTypes.Car
        };
    }
}
