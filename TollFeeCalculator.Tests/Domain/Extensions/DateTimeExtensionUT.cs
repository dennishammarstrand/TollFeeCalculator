using Domain.Extensions;
using System;
using Xunit;

namespace TollFeeCalculator.Tests.Domain.Extensions
{
    public class DateTimeExtensionUT
    {
        [Theory]
        [InlineData("2020-03-03", false)]
        [InlineData("2020-03-01", true)]
        public void ShouldValidateTollFreeDate(string dateString, bool expected)
        {
            //arrange
            var date = DateTime.Parse(dateString);

            //act
            var result = date.IsTollFreeDate();

            //assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2020-03-03", false)]
        [InlineData("2020-03-01", true)]
        public void ShouldValidateWeekendDate(string dateString, bool expected)
        {
            //arrange
            var date = DateTime.Parse(dateString);

            //act
            var result = date.IsWeekend();

            //assert
            Assert.Equal(expected, result);
        }
    }
}
