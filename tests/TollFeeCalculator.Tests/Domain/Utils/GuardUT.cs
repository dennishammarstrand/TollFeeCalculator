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

            //act
            var result = Guard.IsNull(vehicle);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldValidateDatesOfSameDay()
        {
            //arrange
            var dates = new DateTime[] { new DateTime(2020, 03, 03), new DateTime(2020, 03, 04) };

            //act & assert
            Assert.Throws<DateMissmatchException>(() => Guard.DatesOfSameDay(dates));
        }
    }
}
