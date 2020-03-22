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

        [Theory]
        [InlineData("2020-03-03 12:00:00", "2020-03-03 12:30:00", true)]
        [InlineData("2020-03-03 12:00:00", "2020-03-03 13:30:00", false)]
        public void TimeIntervalLessThanAnHour_ShouldValidateInterval(string date1, string date2, bool expected)
        {
            //arrange & act
            var result = DateTimeExtension.TimeIntervalLessThanAnHour(DateTime.Parse(date1), DateTime.Parse(date2));

            //assert
            Assert.Equal(expected, result);
        }
    }
}
