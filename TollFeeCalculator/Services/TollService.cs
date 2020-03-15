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
            var previousTime = orderedDates.First();
            var totalFee = 0;
            var lessThanAnHourRounds = 0;
            var twoPreviousFee = 0;
            foreach (var time in orderedDates)
            {
                var nextFee = GetTollFee(time, vehicle);
                var previousFee = GetTollFee(previousTime, vehicle);

                if (IsTimeIntervalLessThanAnHour(time, previousTime))
                {
                    lessThanAnHourRounds++;
                    if (totalFee > 0) totalFee -= previousFee;
                    totalFee += Math.Max(nextFee, previousFee);
                    _ = lessThanAnHourRounds % 2 == 0 ? twoPreviousFee = previousFee : totalFee += twoPreviousFee;
                }
                else
                {
                    totalFee += nextFee;
                    lessThanAnHourRounds = 0;
                }
                previousTime = time;
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

            var hour = date.Hour;
            var minute = date.Minute;

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
            var diffInMillies = date1.TimeOfDay.TotalMilliseconds - date2.TimeOfDay.TotalMilliseconds;
            var minutes = diffInMillies / 1000 / 60;
            return minutes <= 60;
        }
    }
}