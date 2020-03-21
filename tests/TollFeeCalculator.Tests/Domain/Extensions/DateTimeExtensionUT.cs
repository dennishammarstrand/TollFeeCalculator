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
        [InlineData("2012-03-01", false)]
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

        [Fact]
        public void TimeIntervalLessThanAnHour_ShouldValidateInterval()
        {
            //arrange
            var date1 = new DateTime(2020, 3, 3, 06, 00, 00, 00);
            var date2 = new DateTime(2020, 3, 3, 07, 30, 00, 00);

            //act
            var result = DateTimeExtension.TimeIntervalLessThanAnHour(date1, date2);
        }
    }
}
