using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
