using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using TollFeeCalculator.Tests.Utils;
using Xunit;

namespace TollFeeCalculator.Domain.Services
{
    public class TollServiceUT
    {
        private readonly Mock<ITollFeeRepository> _tollFeeRepository;
        private readonly Mock<IDateService> _dateService;
        private TollService _tollService;
        public TollServiceUT()
        {
            _tollFeeRepository = new Mock<ITollFeeRepository>();
            _dateService = new Mock<IDateService>();
            _tollService = new TollService(_tollFeeRepository.Object, _dateService.Object);
        }


        public class GetTollFee : TollServiceUT
        {
            [Theory]
            [ClassData(typeof(TollServiceTestData))]
            public void ShouldReturnCorrectTollFee(DateTime date, IVehicle vehicle, int expectedValue)
            {
                //arrange 
                _tollFeeRepository.Setup(s => s.GetTollFee(It.IsAny<DateTime>())).Returns(expectedValue);

                //act
                var result = _tollService.GetTollFee(date, vehicle);

                //assert
                Assert.Equal(expectedValue, result);
                _tollFeeRepository.Verify(s => s.GetTollFee(It.IsAny<DateTime>()), Times.Once);
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
            //[Theory]
            //[ClassData(typeof(TotalTollFeeTestData))]
            //public void ShouldCalculateTotalTollFee(DateTime[] dates, IVehicle vehicle, int expected)
            //{
            //    //act
            //    var result = _tollService.GetTotalTollFee(vehicle, dates);

            //    //assert
            //    Assert.Equal(expected, result);
            //}

            [Fact]
            public void ShouldThrowArgumentNullException()
            {
                //arrange
                var car = MockedModels.Car;

                //act & assert
                Assert.Throws<ArgumentNullException>(() => _tollService.GetTotalTollFee(null, new DateTime[] { new DateTime(2020, 03, 03) }));
            }

            [Fact]
            public void ShouldThrowEmptyDateException()
            {
                //arrange
                var car = MockedModels.Car;

                //act & assert
                Assert.Throws<EmptyDateException>(() => _tollService.GetTotalTollFee(car, new DateTime[0]));
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

        public class CalculateFeeForDates : TollServiceUT
        {
            [Fact]
            public void CalculateFeeForDates_ShouldThrowArgumentNullException()
            {
                //act & assert
                Assert.Throws<ArgumentNullException>(() => _tollService.CalculateFeeForDates(new DateTime[] { new DateTime(2020, 03, 03) }, null));
            }

            [Fact]
            public void CalculateFeeForDates_ShouldThrowEmptyDateException()
            {
                //arrange
                var car = MockedModels.Car;

                //act & assert
                Assert.Throws<EmptyDateException>(() => _tollService.CalculateFeeForDates(new DateTime[0], car));
            }

            [Theory]
            [ClassData(typeof(CalculateFeeForDatesTestData))]
            public void CalculateFeeForDates_ShouldReturnListOfDatesAndFees(DateTime[] dates, IVehicle vehicle, int expectedValue)
            {
                //arrange
                _tollFeeRepository.Setup(s => s.GetTollFee(It.IsAny<DateTime>())).Returns(expectedValue);

                //act
                var result = _tollService.CalculateFeeForDates(dates, vehicle);

                //assert
                Assert.Equal(expectedValue, result[0].Item2);
                Assert.Equal(dates[0], result[0].Item1);
                _tollFeeRepository.Verify(s => s.GetTollFee(It.IsAny<DateTime>()), Times.Once);
            }
        }

        public class CalculateTotalTollFee : TollServiceUT
        {
            [Fact]
            public void CalculateTotalTollFee_ShouldThrowArgumentNullException()
            {
                //act & assert
                Assert.Throws<ArgumentNullException>(() => _tollService.CalculateTotalTollFee(null));
            }

            [Fact]
            public void CalculateTotalTollFee_ShouldThrowEmptyDateException()
            {
                //arrange
                var timesAndFees = new List<(DateTime, int)>();

                //act & assert
                Assert.Throws<EmptyDateException>(() => _tollService.CalculateTotalTollFee(timesAndFees));
            }

            [Fact]
            public void CalculateTotalTollFee_ShouldCalculateTotalTollFee()
            {
                //arrange
                var timesAndFees = MockedModels.TimesAndFees;

                //act
                var result = _tollService.CalculateTotalTollFee(timesAndFees);

                //assert
                Assert.Equal(26, result);
            }
        }
    }
}
