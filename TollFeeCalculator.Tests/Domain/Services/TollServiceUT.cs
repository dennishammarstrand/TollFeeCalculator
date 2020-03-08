using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Services;
using System;
using TollFeeCalculator.Tests.Utils;
using Xunit;

namespace TollFeeCalculator.Domain.Services
{
    public class TollServiceUT
    {
        protected TollService _tollService;
        public TollServiceUT()
        {
            _tollService = new TollService();
        }


        public class GetTollFee : TollServiceUT
        {
            [Theory]
            [ClassData(typeof(TollServiceTestData))]
            public void ShouldReturnCorrectTollFee(DateTime date, IVehicle vehicle, int expectedValue)
            {
                //act
                var result = _tollService.GetTollFee(date, vehicle);

                //assert
                Assert.Equal(expectedValue, result);
            }

            [Fact]
            public void ShouldThrowArgumentNullException()
            {
                //arrange & act & assert
                Assert.Throws<ArgumentNullException>(() => _tollService.GetTollFee(new DateTime(2020,03,03), null));
            }
        }

        public class GetTotalTollFee : TollServiceUT
        {
            [Theory]
            [ClassData(typeof(TotalTollFeeTestData))]
            public void ShouldCalculateTotalTollFee(DateTime[] dates, IVehicle vehicle, int expected)
            {
                //act
                var result = _tollService.GetTotalTollFee(vehicle, dates);

                //assert
                Assert.Equal(expected, result);
            }

            [Fact]
            public void ShouldThrowArgumentNullException()
            {
                //arrange & act & assert
                Assert.Throws<ArgumentNullException>(() => _tollService.GetTotalTollFee(null, new DateTime[] { new DateTime(2020, 03, 03) }));
            }

            [Fact]
            public void ShouldThrowDateMissmatchException()
            {
                //arrange
                var car = MockedModels.Car;
                var dates = new DateTime[] { new DateTime(2020,03,03), new DateTime(2020,03,04) };

                //act & assert
                Assert.Throws<DateMissmatchException>(() => _tollService.GetTotalTollFee(car, dates));
            }
        }
    }
}
