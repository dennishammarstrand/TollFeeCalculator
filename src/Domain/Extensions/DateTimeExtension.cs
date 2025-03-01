﻿using System;

namespace Domain.Extensions
{
    public static class DateTimeExtension
    {
        public static bool IsTollFreeDate(this DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            if (date.IsWeekend()) return true;
            if (year >= 2013)
            {
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsWeekend(this DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        public static bool TimeIntervalLessThanAnHour(DateTime firstTime, DateTime secondTime)
        {
            var diffInMillies = firstTime.TimeOfDay.TotalMilliseconds - secondTime.TimeOfDay.TotalMilliseconds;
            var minutes = diffInMillies / 1000 / 60;
            return Math.Abs(minutes) <= 60;
        }
    }
}
