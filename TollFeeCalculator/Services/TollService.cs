using Domain.Entities;
using Domain.Extensions;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Utils;
using System;
using System.Linq;

namespace Domain.Services
{
    public class TollService : ITollService
    {

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTotalTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (Guard.IsNull(vehicle, dates))
            {
                throw new ArgumentNullException();
            }
            Guard.DatesOfSameDay(dates);
            var orderedDates = dates.OrderBy(x => x.TimeOfDay).ToArray();
            DateTime previousTime = orderedDates[0];
            int totalFee = 0;
            int lessThanAnHourRounds = 0;
            int twoPreviousValue = 0;
            foreach (DateTime date in orderedDates)
            {
                int nextFee = GetTollFee(date, vehicle);
                int previousFee = GetTollFee(previousTime, vehicle);

                if (IsTimeIntervalLessThanAnHour(date, previousTime))
                {
                    lessThanAnHourRounds++;
                    if (totalFee > 0) totalFee -= previousFee;
                    totalFee += Math.Max(nextFee, previousFee);
                    if (lessThanAnHourRounds % 2 == 0) twoPreviousValue = previousFee;
                    if (lessThanAnHourRounds % 2 != 0) totalFee += twoPreviousValue;
                }
                else
                {
                    totalFee += nextFee;
                    lessThanAnHourRounds = 0;
                }
                previousTime = date;
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            if (Guard.IsNull(vehicle))
            {
                throw new ArgumentNullException();
            }
            if (date.IsTollFreeDate() || vehicle.GetVehicleType().IsTollFreeVehicle()) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 8 && minute >= 30 || hour > 8 && hour < 15) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 30 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }      
        
        private bool IsTimeIntervalLessThanAnHour(DateTime date1, DateTime date2)
        {
            double diffInMillies = date1.TimeOfDay.TotalMilliseconds - date2.TimeOfDay.TotalMilliseconds;
            double minutes = diffInMillies / 1000 / 60;
            return minutes <= 60;
        }
    }
}