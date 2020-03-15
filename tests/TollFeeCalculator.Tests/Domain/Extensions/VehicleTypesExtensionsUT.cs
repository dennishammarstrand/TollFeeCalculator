using Domain.Enums;
using Domain.Extensions;
using Xunit;

namespace TollFeeCalculator.Tests.Domain.Extensions
{
    public class VehicleTypesExtensionsUT
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(6, false)]
        public void ShouldValidateTollFreeVehicle(int type, bool expected)
        {
            //arrange
            var vehicleType = (VehicleTypes)type;

            //act
            var result = vehicleType.IsTollFreeVehicle();

            //assert
            Assert.Equal(expected, result);
        }
    }
}
