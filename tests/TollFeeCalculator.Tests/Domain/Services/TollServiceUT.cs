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
        private TollService _tollService;
        public TollServiceUT()
        {
            _tollFeeRepository = new Mock<ITollFeeRepository>();
            _tollService = new TollService(_tollFeeRepository.Object);
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
                var result = _tollService.GetTollFeeForDate(date, vehicle);

                //assert
                Assert.Equal(expectedValue, result);
                _tollFeeRepository.Verify(s => s.GetTollFee(It.IsAny<DateTime>()), Times.Once);
            }

            [Fact]
            public void ShouldThrowArgumentNullException()
            {
                //arrange & act & assert
                Assert.Throws<ArgumentNullException>(() => _tollService.GetTollFeeForDate(new DateTime(2020,03,03), null));
            }
        }

        public class GetTotalTollFee : TollServiceUT
        {
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

        public class PairDatesWithFees : TollServiceUT
        {
            [Fact]
            public void PairDatesWithFees_ShouldThrowArgumentNullException()
            {
                //act & assert
                Assert.Throws<ArgumentNullException>(() => _tollService.PairDatesWithFees(new DateTime[] { new DateTime(2020, 03, 03) }, null));
            }

            [Fact]
            public void PairDatesWithFees_ShouldThrowEmptyDateException()
            {
                //arrange
                var car = MockedModels.Car;

                //act & assert
                Assert.Throws<EmptyDateException>(() => _tollService.PairDatesWithFees(new DateTime[0], car));
            }

            [Fact]
            public void PairDatesWithFees_ShouldReturnListOfDatesAndFees()
            {
                //arrange
                var dates = MockedModels.Dates;
                var vehicle = MockedModels.Car;
                _tollFeeRepository.SetupSequence(s => s.GetTollFee(It.IsAny<DateTime>()))
                    .Returns(8)
                    .Returns(13)
                    .Returns(18)
                    .Returns(13);

                //act
                var result = _tollService.PairDatesWithFees(dates, vehicle);

                //assert
                var expected = MockedModels.ExpectedCalculationFeesForDates;
                Assert.Equal(expected, result);
                _tollFeeRepository.Verify(s => s.GetTollFee(It.IsAny<DateTime>()), Times.Exactly(4));
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

            [Theory]
            [ClassData(typeof(CalculateTotalTollFeeTestData))]
            public void CalculateTotalTollFee_ShouldCalculateTotalTollFee(List<(DateTime, int)> timesAndFees, int expected)
            {
                //act
                var result = _tollService.CalculateTotalTollFee(timesAndFees);

                //assert
                Assert.Equal(expected, result);
            }
        }
    }
}
