using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace TollFeeCalculator.Tests.Utils
{
    public static class MockedModels
    {
        public static Car Car => new Car
        {
            Type = VehicleTypes.Car
        };

        public static DateTime[] Dates => new DateTime[]
        {
            new DateTime(2020,3,3,06,00,0),
            new DateTime(2020,3,3,06,30,0),
            new DateTime(2020,3,3,07,00,0),
            new DateTime(2020,3,3,08,00,0)
        };

        public static List<(DateTime, int)> ExpectedCalculationFeesForDates => new List<(DateTime, int)>
        {
            (new DateTime(2020,3,3,06,00,0), 8),
            (new DateTime(2020,3,3,06,30,0), 13),
            (new DateTime(2020,3,3,07,00,0), 18),
            (new DateTime(2020,3,3,08,00,0), 13)
        };

        public static List<(DateTime, int)> TimesAndFees => new List<(DateTime, int)>
        {
            (new DateTime(2020,3,3,06,00,0), 8),
            (new DateTime(2020,3,3,06,30,0), 13),
            (new DateTime(2020,3,3,06,50,0), 13),
            (new DateTime(2020,3,3,07,30,0), 18),
            (new DateTime(2020,3,3,9,00,0), 8)
        };
    }
}
