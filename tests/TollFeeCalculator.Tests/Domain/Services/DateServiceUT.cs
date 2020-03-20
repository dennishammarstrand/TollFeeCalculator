using Domain.Services;
using System;
using Xunit;

namespace TollFeeCalculator.Tests.Domain.Services
{
    public class DateServiceUT
    {
        private DateService _dateService;
        public DateServiceUT()
        {
            _dateService = new DateService();
        }

        public class TimeIntervalLessThanAnHour : DateServiceUT
        {
            [Theory]
            [InlineData("2020-03-03 07:00", "2020-03-03 07:30", true)]
            [InlineData("2020-03-03 07:00", "2020-03-03 08:30", false)]
            public void TimeIntervalLessThanAnHour_ShouldReturnCorrectIntervalCalculation(string date1, string date2, bool expected)
            {
                //arrange
                var firstDate = DateTime.Parse(date1);
                var secondDate = DateTime.Parse(date2);

                //act
                var result = _dateService.TimeIntervalLessThanAnHour(secondDate, firstDate);

                //assert
                Assert.Equal(expected, result);
            }
        }
    }
}
