using Domain.Interfaces.Services;
using System;

namespace Domain.Services
{
    public class DateService : IDateService
    {
        public bool TimeIntervalLessThanAnHour(DateTime firstTime, DateTime secondTime)
        {
            var diffInMillies = firstTime.TimeOfDay.TotalMilliseconds - secondTime.TimeOfDay.TotalMilliseconds;
            var minutes = diffInMillies / 1000 / 60;
            return minutes <= 60;
        }
    }
}
