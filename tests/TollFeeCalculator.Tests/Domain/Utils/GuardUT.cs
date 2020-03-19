using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TollFeeCalculator.Tests.Utils;
using Xunit;

namespace TollFeeCalculator.Tests.Domain.Utils
{
    public class GuardUT
    {
        [Fact]
        public void ShouldCheckForNullObject()
        {
            //arrange
            var vehicle = MockedModels.Car;
            vehicle = null;

            //act & assert
            Assert.Throws<ArgumentNullException>(() => Guard.CheckForNull(vehicle));
        }

        [Fact]
        public void ShouldValidateDatesOfSameDay()
        {
            //arrange
            var dates = new DateTime[] { new DateTime(2020, 03, 03), new DateTime(2020, 03, 04) };

            //act & assert
            Assert.Throws<DateMissmatchException>(() => Guard.ValidateDatesOfSameDay(dates));
        }

        [Fact]
        public void ValidateDatesOfSameDay_ShouldThrowEmptyDateException()
        {
            //arrange
            var dates = new DateTime[0];

            //act & assert
            Assert.Throws<EmptyDateException>(() => Guard.ValidateDatesOfSameDay(dates));
        }
    }
}
